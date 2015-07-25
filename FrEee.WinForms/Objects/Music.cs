using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using FrEee.Utility.Extensions;
using NAudio;
using NAudio.Vorbis;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace FrEee.WinForms.Objects
{
	/// <summary>
	/// Music support for the game.
	/// </summary>
	public static class Music
	{
		private static WaveOutEvent waveout = new WaveOutEvent();

		private static FadeInOutSampleProvider curTrack, prevTrack;

		private static float musicVolume = 1.0f;

		private static MusicMode currentMode;

		private const int FadeDuration = 5000; // milliseconds

		public static bool IsPlaying { get; private set; }

		public static MusicMode CurrentMode
		{
			get
			{
				return currentMode;
			}
			set
			{
				bool newtrack = false;
				if (currentMode != value)
					newtrack = true;
				currentMode = value;
				if (newtrack)
					StartNewTrack();
			}
		}

		private static MusicMood currentMood;

		public static MusicMood CurrentMood
		{
			get
			{
				return currentMood;
			}
			set
			{
				bool newtrack = false;
				if (currentMood != value)
					newtrack = true;
				currentMood = value;
				if (newtrack)
					StartNewTrack();
			}
		}

		private class Track
		{
			public Track(MusicMode mode, MusicMood mood, string path)
			{
				Mode = mode;
				Mood = mood;
				Path = path;
			}

			public MusicMode Mode { get; set; }
			public MusicMood Mood { get; set; }
			public string Path { get; set; }
		}

		private static IEnumerable<Track> FindTracks()
		{
			foreach (MusicMode mode in Enum.GetValues(typeof(MusicMode)))
			{
				if (mode != MusicMode.None)
				{
					foreach (MusicMood mood in Enum.GetValues(typeof(MusicMood)))
					{
						var folder = Path.Combine("Music", mode.ToString(), mood.ToString());
						IEnumerable<string> files = Enumerable.Empty<string>();
						try
						{
							files = Directory.GetFiles(folder, "*.ogg").Union(
								Directory.GetFiles(folder, "*.mp3")).Union(
								Directory.GetFiles(folder, "*.wav"));
						}
						catch
						{
							Console.Error.WriteLine("Cannot find music folder " + folder + ".");
						}
						foreach (var file in files)
							yield return new Track(mode, mood, file);
					}
				}
			}
		}

		public static void StartNewTrack()
		{
			// find out what to play
			var tracks = FindTracks().ToArray();
			var track = tracks.Where(t => t.Mode == CurrentMode && t.Mood == CurrentMood).PickRandom();
			if (track == null)
			{
				// no music? try another mood
				var others = tracks.Where(t => t.Mode == CurrentMode);
				if (others.Any())
					track = others.PickRandom();
			}
			if (track == null)
			{
				// still no music? try another mode!
				track = tracks.PickRandom();
			}
			if (track == null)
			{
				// no music at all :(
				return;
			}

			// prepare the new track
			var tl = track.Path.ToLower();
			WaveChannel32 wc = null;
			if (tl.EndsWith("ogg"))
				wc = new WaveChannel32(new VorbisWaveReader(track.Path));
			else if (tl.EndsWith("mp3"))
				wc = new WaveChannel32(new Mp3FileReader(track.Path));
			else if (tl.EndsWith("wav"))
				wc = new WaveChannel32(new WaveFileReader(track.Path));
			else
				throw new Exception("Unknown audio format for file " + track.Path);

			// convert to a standard format so we can mix them (e.g. a mp3 with an ogg)
			var wf = new WaveFormat();
			var resampler = new MediaFoundationResampler(wc, wf);
			var sp = resampler.ToSampleProvider();

			// setup our track
			wc.Volume = musicVolume;
			wc.PadWithZeroes = false; // to allow PlaybackStopped event to fire
			waveout.PlaybackStopped -= waveout_PlaybackStopped;
			waveout.Stop();
			waveout.Dispose();
			waveout = new WaveOutEvent();
			if (CurrentMode == MusicMode.None)
				return; // no music!

			// fade between the two tracks
			prevTrack = curTrack;
			if (prevTrack != null)
				prevTrack.BeginFadeOut(FadeDuration);
			curTrack = new FadeInOutSampleProvider(sp, true);
			curTrack.BeginFadeIn(FadeDuration);

			// start playing
			// TODO - start fade of new track even before old track is done?
			if (prevTrack != null)
				waveout.Init(new MixingSampleProvider(new ISampleProvider[] { curTrack, prevTrack }));
			else
				waveout.Init(curTrack);
			IsPlaying = true;
			waveout.PlaybackStopped += waveout_PlaybackStopped;
			waveout.Play();
		}

		static void waveout_PlaybackStopped(object sender, StoppedEventArgs e)
		{
			// play another song
			StartNewTrack();
		}

		public static void Play(MusicMode mode, MusicMood mood)
		{
			if (mode == CurrentMode && mood == CurrentMood)
				return;

			currentMode = mode;
			currentMood = mood;
			StartNewTrack();
		}

		public static void setVolume(float volume)
		{
			musicVolume = volume;
		}
	}

	/// <summary>
	/// Types of music to play.
	/// </summary>
	public enum MusicMode
	{
		/// <summary>
		/// Don't play any music!
		/// </summary>
		None = 0,
		/// <summary>
		/// Music to play at the main menu.
		/// </summary>
		Menu,
		/// <summary>
		/// Music to play during the strategic game.
		/// </summary>
		Strategic,
		/// <summary>
		/// Music to play during combat.
		/// </summary>
		Combat,
	}

	/// <summary>
	/// Music mood (tense, upbeat, etc.)
	/// </summary>
	public enum MusicMood
	{
		Peaceful = 0,
		Tense,
		Upbeat,
		Sad,
	}
}
