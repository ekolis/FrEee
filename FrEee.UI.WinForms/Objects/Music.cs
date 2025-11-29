using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Providers;
using SoundFlow.Structs;

namespace FrEee.UI.WinForms.Objects;

using Console = System.Console;

/// <summary>
/// Music support for the game.
/// </summary>
public static class Music
{
	static Music()
	{
		try
		{
			engine = new MiniAudioEngine();
			engine.UpdateAudioDevicesInfo();
			var defaultDevice = engine.PlaybackDevices.FirstOrDefault(x => x.IsDefault);
			playbackDevice = engine.InitializePlaybackDevice(defaultDevice, format);
			playbackDevice.Start();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error initializing music; disabling it");
			Console.WriteLine("See errorlog.txt for details.");
			disableMusic = true;
			ex.Log();
		}
	}
	
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
	private static float musicVolume = 1.0f;
	private static bool disableMusic = false;

	// SoundFlow implementation variables
	private static MiniAudioEngine engine;
	private static AudioFormat format = AudioFormat.DvdHq;
	private static AudioPlaybackDevice playbackDevice;
	private static SoundPlayer player;

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
		using var dataProvider = new StreamDataProvider(engine, format, File.OpenRead(track.Path));
		var newPlayer = new SoundPlayer(engine, format, dataProvider);
		newPlayer.IsLooping = true;
		newPlayer.Volume = musicVolume;
		
		// stop old track
		// TODO: fade music
		player.Stop();
		playbackDevice.MasterMixer.RemoveComponent(player);
		
		// start new track
		player = newPlayer;
		playbackDevice.MasterMixer.AddComponent(player);
		player.Play();
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