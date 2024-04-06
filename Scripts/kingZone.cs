using Godot;
using System;
using System.Diagnostics;

public partial class kingZone : cardZone
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetColBox();
	}

	
	public override void CardIntake(Node card)
	{
		GD.Print("MOVE CARD AAA");
		cardList.Add(card);
		ReorderCards();	
		card.Call("ZoneTransfer",this, (int)card.Call("GetZIndex"));
		//Add to score
		scoreLabel.OnCardMoveToKingZone();
		//todo: differentiate from sources to apply different score
		// if(Source == kingZone){
		// 	scoreLabel.OnCardMoveBetweenKingZones();
		// }else{
		// 	scoreLabel.OnCardMoveToKingZone();
		// }
	}

	public void CardIntakeDeal(Node card)
	{
		cardList.Add(card);
		ReorderCards();	
		card.Call("ZoneTransfer",this, (int)card.Call("GetZIndex"), true);
		
	}

	public bool RuleCheck(int incomingValue)
	{
		if (incomingValue==13)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool RuleCheck(int incomingValue, int incomingSuit, int topSuit,int topValue)
	{
		if (incomingValue == topValue-1)
		{
			if ((incomingSuit <=1 && topSuit >= 2)||(incomingSuit >=2 && topSuit <= 1))
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
		}
	}		


	public void RevealTopCard()
	{
		Node card;
		int topCard=cardList.Count-1;

		if (topCard>=0)
		{	
		//Calls the card in list's method to Reveal it... The reveal method moves it as well, and puts it in the "DrawPile" or whatever it's called.
		cardList[topCard].Call("FlipCard");
		card = cardList[topCard];
		//More drawing order shenanigans... Don't know if it's neccesary or not. Seems to work as is. I'll come back later.
	//	MoveChild(cardList[topCard],51);
		//Remove card from the deck list... Need to add it to the DrawPile list later....
		//cardList.RemoveAt(topCard);
		//drawZone.Call("CardIntake", card);
		}

		//Add to score
		scoreLabel.OnCardTurnedFaceUp();
	}

	public Boolean IsTopCardRevealed()
	{
		int topCard=cardList.Count-1;
		return (bool)cardList[topCard].Call("AmIRevealed");
	}



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
