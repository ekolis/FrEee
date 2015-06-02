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

namespace FrEee.WinForms.Objects
{
	/// <summary>
	/// Music support for the game.
	/// </summary>
	public static class Music
	{
		private static WaveOut waveout;

		private static MusicMode currentMode;

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

		public static void StartNewTrack()
		{
			// find out what to play
			var trackname = ClientSettings.Instance.Music[Tuple.Create(CurrentMode, CurrentMood)].PickRandom();
			if (trackname == null)
			{
				// no music? try another mood
				var hasMusic = ClientSettings.Instance.Music.Where(kvp => kvp.Key.Item1 == CurrentMode && kvp.Value.Any());
				if (hasMusic.Any())
					trackname = hasMusic.PickRandom().Value.PickRandom();
			}
			if (trackname == null)
			{
				// still no music? try another mode!
				var hasMusic = ClientSettings.Instance.Music.Where(kvp => kvp.Value.Any());
				if (hasMusic.Any())
					trackname = hasMusic.PickRandom().Value.PickRandom();
			}
			if (trackname == null)
			{
				// no music at all :(
				return;
			}

			// play it!
			// TODO - fade effect
			var tl = trackname.ToLower();
			var path = Path.Combine("Music", trackname);
			IWaveProvider p;
			if (tl.EndsWith("ogg"))
				p = new VorbisWaveReader(path);
			else if (tl.EndsWith("mp3"))
				p = new Mp3FileReader(path);
			else if (tl.EndsWith("wav"))
				p = new WaveFileReader(path);
			else
				throw new Exception("Unknown audio format for file " + path);
			if (waveout != null)
			{
				waveout.Stop();
				waveout.Dispose();
				waveout = null;
			}
			if (CurrentMode == MusicMode.None)
				return; // no music!
			waveout = new WaveOut();
			waveout.Init(p);
			waveout.Play();
			waveout.PlaybackStopped += waveout_PlaybackStopped;
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
