using FrEee.Extensions;
using NAudio.Vorbis;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrEee.UI.WinForms.Objects;

/// <summary>
/// Music support for the game.
/// </summary>
public static class Music
{
	static Music()
	{
		try
		{
			OperatingSystem os = Environment.OSVersion;
			PlatformID pid = os.Platform;
			if (pid == PlatformID.Unix)
			{
				Console.WriteLine("Linux detected, disabling Music");
				disableMusic = true;
				return;
			}

			waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);
			mixer = new MixingSampleProvider(waveFormat);

			waveout.Init(mixer);
			waveout.PlaybackStopped += waveout_PlaybackStopped;
			waveout.Play();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error initializing music; disabling it");
			Console.WriteLine("See errorlog.txt for details.");
			disableMusic = true;
			ex.Log();
		}
	}

	// milliseconds
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

	public static bool IsPlaying { get; private set; }
	private const int FadeDuration = 5000;
	private static MusicMode currentMode;
	private static MusicMood currentMood;
	private static FadeInOutSampleProvider curTrack, prevTrack;
	private static MixingSampleProvider mixer;
	private static float musicVolume = 1.0f;
	private static WaveFormat waveFormat;
	private static WaveOutEvent waveout = new WaveOutEvent();
	private static bool disableMusic = false;

	public static void Play(MusicMode mode, MusicMood mood)
	{
		if (mode == CurrentMode && mood == CurrentMood)
			return;
		if (disableMusic) 
			return;

		currentMode = mode;
		currentMood = mood;
		StartNewTrack();
	}

	public static void setVolume(float volume)
	{
		if (disableMusic)
                return;
		musicVolume = volume;
	}

	public static void StartNewTrack()
	{
		if (disableMusic)
                return;
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
		else if (tl.EndsWith("aiff") || tl.EndsWith("aif"))
			wc = new WaveChannel32(new AiffFileReader(track.Path));
		else
			throw new Exception("Unknown audio format for file " + track.Path);

		// convert to a standard format so we can mix them (e.g. a mp3 with an ogg)
		var resampler = new MediaFoundationResampler(wc, waveFormat);
		var sp = resampler.ToSampleProvider();

		// setup our track
		wc.Volume = musicVolume;
		wc.PadWithZeroes = false; // to allow PlaybackStopped event to fire
		if (CurrentMode == MusicMode.None)
			return; // no music!

		// fade between the two tracks
		mixer.RemoveMixerInput(prevTrack);
		prevTrack = curTrack;
		if (prevTrack != null)
			prevTrack.BeginFadeOut(FadeDuration);
		curTrack = new FadeInOutSampleProvider(sp, true);
		curTrack.BeginFadeIn(FadeDuration);
		mixer.AddMixerInput(curTrack);
		waveout.Play();
		IsPlaying = true;
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
						files = Directory.GetFiles(folder, "*.ogg").Concat(
							Directory.GetFiles(folder, "*.mp3")).Concat(
							Directory.GetFiles(folder, "*.wav")).Concat(
							Directory.GetFiles(folder, "*.aiff")).Concat(
							Directory.GetFiles(folder, "*.aif"));
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

	private static void waveout_PlaybackStopped(object sender, StoppedEventArgs e)
	{
		// play another song
		StartNewTrack();
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

	/// <summary>
	/// Music to play when the game is over (victory or defeat).
	/// </summary>
	GameOver,
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
