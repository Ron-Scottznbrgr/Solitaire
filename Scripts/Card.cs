using Godot;
using System;

public partial class Card : Node2D
{
	[Export]
	[ExportCategory("Card Data")]
	//[ExportHint(EnumHint.Category)]
	public int CardValue; // Ace = 1, Jack = 11, Queen = 12, King = 13

	[Export]
		[ExportCategory("Card Data")]
	//[ExportHint(EnumHint.Category)]
	public int CardSuit; // 0 = Hearts, 1 = Diamonds, 2 = Clubs, 3 = Spades

	[Export]
	[ExportCategory("Card Images")]
	private Texture cardBackTexture; // Path to back of card image

	[Export]
	[ExportCategory("Card Images")]
	private Texture cardFrontTexture; // Path to front of card image

	private bool isFaceUp = false; // Tracks the face-up status of the card

	// Path to the sprite node within this Node2D
	private Sprite2D cardImage;

	public override void _Ready()
	{
		cardImage = GetNode<Sprite2D>("CardImage");
		UpdateCardFace(); // make sure to define this method
	}

	// Method to flip the card
	public void FlipCard()
	{
		isFaceUp = !isFaceUp;
		UpdateCardFace(); // make sure to define this method
	}

	// Update the card's visual based on its state
	private void UpdateCardFace()
	{
		//Sprite2D.Texture = isFaceUp ? cardFrontTexture : cardBackTexture;
		
		// This method should update the cardImage.Texture based on isFaceUp
		// and the card textures provided.
	}

	// Property to determine if the card is face up
	public bool IsFaceUp
	{
		get => isFaceUp;
		set
		{
			isFaceUp = value;
			UpdateCardFace(); // have to make sure to define this method
		}
	}

	public Color CardColor
	{
		get => (CardSuit == 0 || CardSuit == 1) ? Colors.Red : Colors.Black;
	}

	public void SetCardData(int CardValue, int CardSuit, Texture frontTexture = null, Texture backTexture = null)
	{
		String suit="";
		//CardValue = value;
		//CardSuit = suit;

if (CardSuit == 0)
		{
			suit = "Hearts";
		}
		else if (CardSuit == 1)
		{
			suit = "Diamonds";
		}
		else if (CardSuit == 2)
		{
			suit = "Clubs";
		}
		else if (CardSuit ==3)
		{
			suit = "Spades";
		}

		//setting the path to the image...
		//loading the texture from the path
		cardFrontTexture.Texture = (Texture2D)GD.Load("res://Assets/Images/Cards/"+suit+"/"+CardValue+".png");
		//error:/Users/bluemeaford/Desktop/Solitaire/Scripts/Card.cs(93,20): 'Texture' does not contain a definition for 'Texture' and no accessible extension method 'Texture' accepting a first argument of type 'Texture' could be found (are you missing a using directive or an assembly reference?)
		if (frontTexture != null)
		{
			cardFrontTexture = frontTexture;
		}

		if (backTexture != null)
		{
			cardBackTexture = backTexture;
		}

		UpdateCardFace(); // need to define this method
	}


	// This method allows setting the position of the card
	public void SetCardPos(Vector2 pos)
	{
		GlobalPosition = pos;
	}
}



















