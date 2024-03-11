using Godot;
using System;
using System.Collections.Generic;
using System.Net.Security;

public partial class cardZone : Node2D
{

	public List<Node> cardList = new List<Node>(52);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void SetPos(Vector2 newPos)
	{
		this.GlobalPosition = newPos;

		//GD.Print(newPos + "   //////   drawpile          positions");		
	}


	public virtual void CardIntake(Node card)
	{
		cardList.ForEach(c => { c.Call("SetSelectable", false); });
		cardList.Add(card);
		ReorderCards();
		card.Call("ZoneTransfer", this.GlobalPosition);

		/*
			GD.Print(""+this.Name + " is accepting a card...");
			foreach(Node cardInList in cardList)
			{
				cardInList.Call("DebugPrintCardToConsole");
			}
			*/

	}

	public virtual void CardOuttake()
	{
		if (cardList.Count == 0)
		{
			return;
		}
		int topCard = cardList.Count;
		GD.Print("" + this.Name + ", which has " + topCard + " cards in it, is removing a card...");

		//cardList[topCard-1].Call("SetZIndex",10);
		cardList.RemoveAt(topCard - 1);
		cardList[cardList.Count - 1].Call("SetSelectable", true);

	}

	public virtual void RemoveCard(Node card)
	{
		if (!HasCards()) return;
		cardList.Remove(card);
		if (HasCards())
		{
			cardList.ForEach(c => { c.Call("SetSelectable", false); });
			cardList[cardList.Count - 1].Call("SetSelectable", true);
		}
	}

	public virtual void MoveCardtoZone(Node2D targetZone)
	{

		int topCard = cardList.Count;

		if (cardList.Count > 0)
		{
			GD.Print("Trying to remove a card from " + this.Name);
			//cardList[topCard-1].Call("DebugPrintCardToConsole");
			targetZone.Call("HasCards");
			targetZone.Call("CardIntake", cardList[topCard - 1]);
			targetZone.Call("HasCards");
			CardOuttake();
			this.Call("HasCards");
			GD.Print("" + this.Name + " is Moving a card to Zone " + targetZone.Name);
		}
	}

	public virtual void MoveCardtoZone(Node2D targetZone, bool drawZoneEmpty)
	{
		int topCard = cardList.Count;
		GD.Print("Cards in " + this.Name + "  = " + topCard);
		if (cardList.Count > 0)
		{
			foreach (Node cardInList in cardList)
			{
				GD.Print("Looping");
				cardInList.Call("FlipCard");

				//GD.Print("Trying to remove a card from "+this.Name);
				//cardList[topCard-1].Call("DebugPrintCardToConsole");
				//targetZone.Call("HasCards");
				targetZone.Call("CardIntake", cardInList);
				//targetZone.Call("HasCards");
				//this.Call("HasCards");
				//GD.Print(""+this.Name+" is Moving a card to Zone "+targetZone.Name);
			}
			ClearZone();
		}
	}

	public virtual void ReorderCards()
	{
		int cardZindex;
		cardZindex = 0;


		foreach (Node cardInList in cardList)
		{
			cardInList.Call("SetZIndex", cardZindex);
			cardZindex += 1;
		}

	}


	public bool HasCards()
	{
		if (cardList.Count > 0)
		{
			//GD.Print ("" +this.Name +" HAS "+cardList.Count+ " cards in it.");
			return true;
		}
		else
		{
			//GD.Print ("" +this.Name +" HAS "+cardList.Count+ " cards in it.");
			return false;
		}
	}


	public virtual int TopCardSuit()
	{
		int topCard = cardList.Count;
		int suit = -999;

		if (cardList.Count > 0)
		{
			suit = (int)cardList[topCard - 1].Call("GetCardSuit");
		}
		GD.Print(this.Name + "card count is currently " + topCard);
		GD.Print(this.Name + "top suit is " + suit);
		return suit;
	}

	public virtual int TopCardValue()
	{
		int topCard = cardList.Count;
		int value = -999;

		if (cardList.Count > 0)
		{
			value = (int)cardList[topCard - 1].Call("GetCardValue");
		}
		GD.Print(this.Name + "card count is currently " + topCard);
		GD.Print(this.Name + "top value is " + value);
		return value;
	}

	public virtual bool RuleCheck(Card card)
	{
		return true;
	}

	public void ClearZone()
	{
		cardList.Clear();
	}

	public void OnCardExit(Node2D body)
	{
		//TODO:: add remove card stat here
	}

}
