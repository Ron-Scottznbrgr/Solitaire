using Godot;
using System;

public partial class kingZone : cardZone
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public override void CardIntake(Node card)
	{
		cardList.Add(card);
		ReorderCards();	
		card.Call("ZoneTransfer",this, (int)card.Call("GetZIndex"));

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
