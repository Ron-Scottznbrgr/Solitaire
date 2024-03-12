using Godot;
using System;


public partial class mouseZone : cardZone
{

	Vector2 mousePosition;
		public ColorRect cardCounterRect; 	// Box to put Text on to Display Num of Cards.
	public Label cardCountLabel;		//Counter for cards in Deck.
	int cardCountInt=0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardCountLabel = GetNode<Label>("CountLabel");
		cardCounterRect = GetNode<ColorRect>("CountRect");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mousePosition = GetGlobalMousePosition();

		this.GlobalPosition = mousePosition;
		//GD.Print("Cards in Mouse: "+cardList.Count);
		cardCountInt = cardList.Count; 
		cardCountLabel.Text = ("   Card Count: "+cardCountInt);

	}


	public override void CardIntake(Node card)
	{
		card.Call("AddToMouse");
		cardList.Add(card);
		ReorderCards();	
		card.Call("ZoneTransfer",this, (int)card.Call("GetZIndex"));

	}

	public override void CardOuttake()
	{
		int topCard=cardList.Count;
		GD.Print(""+this.Name + ", which has "+topCard+" cards in it, is removing a card...");
		
		//cardList[topCard-1].Call("SetZIndex",10);
		
		cardList.RemoveAt(topCard-1);
		


	}


	public override void MoveCardtoZone(Node2D targetZone)
	{
		
		int topCard=cardList.Count;

		if (cardList.Count > 0)
		{
			GD.Print("Trying to remove a card from "+this.Name);
			//cardList[topCard-1].Call("DebugPrintCardToConsole");
			targetZone.Call("HasCards");
			cardList[topCard-1].Call("RemoveFromMouse");
			targetZone.Call("CardIntake",cardList[topCard-1]);
			targetZone.Call("HasCards");
			CardOuttake();
			this.Call("HasCards");
			GD.Print(""+this.Name+" is Moving a card to Zone "+targetZone.Name);
		}
	}


	public override void MoveCardtoZone(Node2D targetZone, Boolean isReturning)
	{
		
		int topCard=cardList.Count;

		if (cardList.Count > 0)
		{
			GD.Print("Trying to remove a card from "+this.Name);
			//cardList[topCard-1].Call("DebugPrintCardToConsole");
			cardList[topCard-1].Call("RemoveFromMouse");
			targetZone.Call("CardIntake",cardList[topCard-1],true);
			CardOuttake();
			this.Call("HasCards");
			GD.Print(""+this.Name+" is Moving a card to Zone "+targetZone.Name);
		}
	}


	/*public bool RuleCheck(int incomingValue)
	{
		
	}

	public bool RuleCheck(int incomingValue, int incomingSuit, int topSuit,int topValue)
	{
		
	}*/		
		/*
		int suit = (int)card.Call("GetCardSuit");
		int value = (int)card.Call("GetCardValue");

		if (value == topValue-1)
		{
			if ((suit <=1 && topSuit >= 2)||(suit >=2 && topSuit <= 1))
			{
				return true;
			}
			else
			{
				return false;//error noise
			}
		}
		else
		{
			return false;//error noise
		}*/
	


}
