using Godot;
using System;

public partial class SFX_Player : AudioStreamPlayer
{
	AudioStreamPlayer SFX_audioNode;
	AudioStream SFX_cardFlip;
	AudioStream SFX_cardError;
    AudioStream SFX_cardPlace;
	AudioStream SFX_cardShove;
    AudioStream SFX_happyHorn;
    AudioStream SFX_shuffle;


	public override void _Ready()
	{
        //Node Itself
        SFX_audioNode = GetNode<AudioStreamPlayer>("../SFX");

        //Calls when ard is drawn
		SFX_cardFlip = (AudioStream)ResourceLoader.Load("res://Assets/Audio/cardDraw.ogg");
		//Calls when card cannot be put into the requested zone
        //SFX_cardError = (AudioStream)ResourceLoader.Load("res://Art/Audio/cardError.ogg");
        //Calls when deck is shuffled
        SFX_shuffle = (AudioStream)ResourceLoader.Load("res://Assets/Audio/cardShuffle.ogg");
        //Calls when card goes into Ace Zone
        //SFX_happyHorn = (AudioStream)ResourceLoader.Load("res://Art/Audio/.mp3");
        //Calls when a card is placed into any zone
        SFX_cardPlace = (AudioStream)ResourceLoader.Load("res://Assets/Audio/cardPlace.ogg");
		SFX_cardShove = (AudioStream)ResourceLoader.Load("res://Assets/Audio/cardShove.ogg");
	}

	public override void _Process(double delta)
	{

	}




	public void SFX_Shuffle()
	{
		SFX_audioNode.Stop();
		SFX_audioNode.Stream = SFX_shuffle;
		SFX_audioNode.Play();
	}
	public void SFX_CardDraw()
	{
		SFX_audioNode.Stop();
		SFX_audioNode.Stream = SFX_cardFlip;
		SFX_audioNode.Play();
	}
   
   /* public void SFX_Error()
	{
		SFX_audioNode.Stop();
		SFX_audioNode.Stream = SFX_cardError;
		SFX_audioNode.Play();
	}*/
    public void SFX_CardPlace()
	{
		SFX_audioNode.Stop();
		SFX_audioNode.Stream = SFX_cardPlace;
		SFX_audioNode.Play();
	}
	   public void SFX_CardShove()
	{
		SFX_audioNode.Stop();
		SFX_audioNode.Stream = SFX_cardShove;
		SFX_audioNode.Play();
	}


}
