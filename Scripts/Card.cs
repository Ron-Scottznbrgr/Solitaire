using Godot;
using System;

public partial class Card : Node2D
{
	//Export just puts the data in the inspector for the cards... you can set categories. 
	//The formatting is a bit weird, but it's helpful in the long run
	[Export]
    [ExportCategory("Card Data")]
	private int cardValue;		// Ace = 1
								// Jack = 11
								// Queen = 12
								// King = 13
	
	
	[Export]
	private int cardSuit;		// 0 = Hearts
								// 1 = Diamonds
								// 2 = Clubs
								// 3 = Spades
	
	[Export]
    [ExportCategory("Card Images")]
	private Sprite2D cardBackImage; 	// Path to back of card image... Not used yet. 

	[Export]
	public Sprite2D cardFrontImage; 	// Empty by default, assigned an image on card creation.

	private Boolean isFaceUp;		//Is the card face up.. not used yet.


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Finding the reference to the Sprite2D image node named "CardImage" under the main card node.
		cardFrontImage = GetNode<Sprite2D>("CardImage");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Not implemented this yet.
	public void GetCardData()
	{
	}

	public void SetCardData(int cardValue, int cardSuit)
	{
		//This suit variable is just to help with linking images to the card
		//I didn't want to use "Hearts" and stuff in strings to compare,  0 & 1 are red, 2 & 3 are black.
		//Easy to remember, less typing :P
		//Open to change if needed.
		String suit="";
					

		//Setting the suit based on... suit...
								// 0 = Hearts
								// 1 = Diamonds
								// 2 = Clubs
								// 3 = Spades

		if (cardSuit == 0)
		{
			suit = "Hearts";
		}
		else if (cardSuit == 1)
		{
			suit = "Diamonds";
		}
		else if (cardSuit == 2)
		{
			suit = "Clubs";
		}
		else if (cardSuit ==3)
		{
			suit = "Spades";
		}

		//setting the path to the image...
		//loading the texture from the path
		cardFrontImage.Texture = (Texture2D)GD.Load("res://Assets/Images/Cards/"+suit+"/"+cardValue+".png");
		
		//More debug text
		//GD.Print(suit);
		//GD.Print(cardValue);
	}	

	//Sets the card position
	public void SetCardPos(Vector2 pos)
	{
		this.GlobalPosition = pos;	
	}

}
