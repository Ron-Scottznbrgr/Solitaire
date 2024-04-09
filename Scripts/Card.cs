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

	public AudioStreamPlayer SFX;
	private Boolean isFaceUp;		//Is the card face up..
	private Boolean isRevealing;		//Is the card moving around? Used in revealing card;
	private Boolean isTransferring;		//Is the card moving around between zones
	private Vector2 revealPilePosition; //The position of the pile of cards the player can see.

	private Vector2 smallestScale;	//Used for Card Flipping
	private Vector2 defaultScale;	//Base Scale of the Card. Returns to this after Flip.

	private Vector2 targetpos;	//Used for moving to zones

	private int restingZIndex=0;
	
	private float flipSpeed;		//Speed in which a card flips over
	private float moveSpeed;		//Speed in which a card moves from 1 pos to another while revealing;
	private int zoneSizeOffset = 7;
	private int offsetX,offsetY;	//Debug Stacking
	public Boolean isOnMouse=false;
	private Node2D mouseZone;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Finding the reference to the Sprite2D image node named "CardImage" under the main card node.
		cardFrontImage = GetNode<Sprite2D>("CardImage");
		cardBackImage = GetNode<Sprite2D>("BackImage");
		mouseZone = GetNode<Node2D>("../../mouseZone");
		SFX = GetNode<AudioStreamPlayer>("../../SFX");

		//Give the card a back image...
		cardBackImage.Texture = (Texture2D)GD.Load("res://Assets/Images/Cards/back_alt.png");
		isFaceUp=false;
		isRevealing=false;
		isTransferring=false;
        smallestScale = new Vector2(0.01f,1f);		//scale to make the card look like it's flipping over.
		defaultScale = this.Scale;					//scale the card started at
		flipSpeed = 0.05f;							//Speed in which the card flips. I like 0.05f. Subject to change
		moveSpeed = 10f;							//Speed in which the card moves. I like 15.0f. Subject to Change.
		//FlipCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		

		//If the card is set to move...
		if (isRevealing)
		{
			//If it hasn't reached it's destination or correct size back at the default after a flip...
			if ((GlobalPosition==revealPilePosition)&&(Scale==defaultScale))
			{
				//GD.Print("Arrived at Destination");
				isRevealing=false;
			}
			else
			{
				//GD.Print("Attempting to Move Card");
				//GD.Print(revealPilePosition);
				MoveCard(revealPilePosition);
			}
			return;
		}		


		if (isTransferring)
		{
		if (GlobalPosition!=targetpos)
		{
			//Get into Position
			this.GlobalPosition = this.GlobalPosition.MoveToward(targetpos, moveSpeed);
			

			//GD.Print("========Moving to target:::   "+GlobalPosition+"  ///  "+targetPos);
			//GD.Print("Face:  "+isFaceUp+"  //  Scale: "+this.Scale+"     //   Default:   "+ defaultScale+"   //  Small:    "+smallestScale);
		}
		else
		{
			this.isTransferring = false;
			SFX.Call("SFX_CardPlace");
			SetZIndex(restingZIndex);
			
		}
		}

		if (isOnMouse)
		{
			GlobalPosition = mouseZone.GlobalPosition;
		}

	}

	public void AddToMouse()
	{
		isOnMouse=true;
	}
	public void RemoveFromMouse()
	{
		isOnMouse=false;
	}

	//Not implemented this yet.
	public int GetCardSuit()
	{
		return cardSuit;
	}
	public int GetCardValue()
	{
		return cardValue;
	}
	
	public void FlipCard()
	{
		//Simply flips the card. If it's face up, it turns face down, and the reverse as well. 
		//Will set the visibility of the back of the card based on this as well.
		//
		//Currently, if the card back is over top of the front. So if you remove the visibility of the card back, the front is visible.
		//It's like 2 "cards" on top of each other. Back then Front. Remove the back, and you can see the front! It's a bit weird to wrap your brain around.
		if (isFaceUp)
		{
			isFaceUp = false;						
		}
		else
		{
			isFaceUp = true;
		}
		cardBackImage.Visible = !isFaceUp;
		
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
		
		//Forgot to do this in the initial commit, and then I couldn't figure out why I couldn't see the value of the cards 	(҂◡_◡) ᕤ
		this.cardValue = cardValue;
		this.cardSuit = cardSuit;


		//More debug text
		//GD.Print(suit);
		//GD.Print(cardValue);
	}	

	//Sets the card position
	public void SetCardPosition(Vector2 pos, Vector2 revealPos)
	{
		this.GlobalPosition = pos;	
		revealPilePosition = revealPos;
	}

	
	//Overload of the SetCardPosition above cuz I'm a bit lazy, and not sure where this is all heading.
	//This one takes an XY offset from position in the deck, and reverses it, so they stack nicely in the draw pile.
	public void SetCardPosition(Vector2 pos, Vector2 revealPos, int offsetX, int offsetY)
	{
		this.GlobalPosition = pos;	
		this.revealPilePosition = revealPos;
		
		//this.revealPilePosition = new Vector2 (((revealPos.X)+-offsetX*2), (revealPos.Y)+-offsetY*2);
		

		//revealPilePosition = (revealPos);//+ new Vector2((-offsetX*2),(-offsetY*2)));
		//GD.Print(revealPos + "   //////   "+revealPilePosition+"          positions");
	}

	//Setting the DrawPile position recieved from the deck.
	public void SetRevealPilePosition(Vector2 pos)
	{
		this.revealPilePosition = pos;
	}

	//The call that activates the reveal card.
	public void RevealCard()
	{		
		if (this.isRevealing==false)
		{
		//GD.Print("Setting card to Move = True....");
		this.isRevealing=true;
		}
	}

	//This is where the revealing animation happens....
	public void MoveCard(Vector2 targetPos)
	{
		//If it's not in position...
		if (GlobalPosition!=targetPos)
		{
			//Get into Position
			this.GlobalPosition = this.GlobalPosition.MoveToward(targetPos, moveSpeed);
			

			//GD.Print("========Moving to target:::   "+GlobalPosition+"  ///  "+targetPos);
			//GD.Print("Face:  "+isFaceUp+"  //  Scale: "+this.Scale+"     //   Default:   "+ defaultScale+"   //  Small:    "+smallestScale);
		}
		
		
		if (isFaceUp)
			{
				if (this.Scale == defaultScale)
				{
				
				}
				else
				{
					//if it's not to Scale, get to Scale
					this.Scale = this.Scale.MoveToward(defaultScale,flipSpeed);
				}
			}
			else
			{				
				if (this.Scale == smallestScale)
				{
					//If it has become as small as we want, and it's still "facedown", make it face up
					FlipCard();
				}
				else
				{
					//Shrink until we say so.
					this.Scale = this.Scale.MoveToward((smallestScale),flipSpeed);
				}
			}
				

		if (GlobalPosition==targetPos && Scale == defaultScale && isFaceUp)
		{
			//If it's finished moving, and changing size AND it's face up, then we are done moving to the draw pile.
			this.isRevealing=false;
			//GD.Print("Finished Moving");
			GD.Print("the card is at "+this.GlobalPosition);
		}
	}


	public void ZoneTransfer(Node2D zone)
	{
		this.isTransferring = true;
		Vector2 zonePos = zone.GlobalPosition;
		targetpos = new Vector2(zone.GlobalPosition.X,zone.GlobalPosition.Y);
		cardFrontImage.ZIndex=1001;
	}
public void ZoneTransfer(Node2D zone, int yOffset, Boolean Dealing)
{
		this.isTransferring = true;
		Vector2 zonePos = zone.GlobalPosition;
		targetpos = new Vector2(zone.GlobalPosition.X,(zone.GlobalPosition.Y+(50*yOffset)));
		SetZIndexFrontAndBack(50*yOffset);
}


	public void ZoneTransfer(Node2D zone, int yOffset)//for King Zones, stacks the cards.
	{
		this.isTransferring = true;
		//GD.Print ("ZINDEX IS =  "+yOffset);
		targetpos = new Vector2(zone.GlobalPosition.X,(zone.GlobalPosition.Y+(50*yOffset)));
		cardFrontImage.ZIndex=1001;
	}

	public void SetZIndex(int newZIndex)
	{
		//GD.Print ("Setting Z index to "+newZIndex+"   from "+cardFrontImage.ZIndex);
		cardBackImage.ZIndex=newZIndex;
		cardFrontImage.ZIndex=newZIndex;
		restingZIndex = newZIndex;
	}
public void SetZIndexFrontAndBack(int newZIndex)
	{
		//GD.Print ("Setting Z index to "+newZIndex+"   from "+cardFrontImage.ZIndex);
		cardFrontImage.ZIndex=newZIndex;
		cardBackImage.ZIndex=newZIndex;
		restingZIndex = newZIndex;
	}



	public int GetZIndex()
	{
		return this.cardFrontImage.ZIndex;
	}

	public void SetFanPosition (int yOffset)
	{
		this.GlobalPosition = new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y+(10*yOffset));
	}

	public Boolean AmIRevealed()
	{
		return isFaceUp;
	}

	public void DebugPrintCardToConsole()
	{
		String suit="";

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

		GD.Print(suit);
		GD.Print(cardValue);
	}




}
