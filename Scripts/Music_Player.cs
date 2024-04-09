using Godot;
using System;

public partial class Music_Player : AudioStreamPlayer
{
	AudioStreamPlayer BG_audioNode;
	AudioStream music;
	AudioStream winMusic;
	Boolean win=false;
	
	public override void _Ready()
	{
		BG_audioNode = GetNode<AudioStreamPlayer>("../BG_Music");
		music = (AudioStream)ResourceLoader.Load("res://Assets/Audio/Pokemon-Game_Corner.mp3");
		winMusic = (AudioStream)ResourceLoader.Load("res://Assets/Audio/FFT-Fanfare.mp3");
	}

	public override void _Process(double delta)
	{
	    if (this.Playing==false && win==false)
		{
			this.Play();
		}
	}

	public void PlayMusic()
	{
		BG_audioNode.Stop();
		BG_audioNode.Stream = music;
		BG_audioNode.Play();
	}
	public void WinMusic()
	{
		BG_audioNode.Stop();
		BG_audioNode.Stream = winMusic;
		win = true;
		BG_audioNode.Play();
	}
}
