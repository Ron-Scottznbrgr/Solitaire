using Godot;
using System;

public partial class aceZone : cardZone
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetColBox();
	}

	

	public bool RuleCheck(int incomingValue)
	{
		if (incomingValue==1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}


	public bool RuleCheck(int incomingValue, int incomingSuit, int topSuit, int topValue)
	{
		if (incomingSuit==topSuit)
		{
			if (incomingValue==(topValue+1))
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



		/*
		if ((int)card.Call("GetCardSuit")==topSuit)
		{
			if ((int)card.Call("GetCardValue")==(topSuit+1))
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
		

		

	/*
		GD.Print(""+this.Name + " is accepting a card...");
		foreach(Node cardInList in cardList)
		{
			cardInList.Call("DebugPrintCardToConsole");
		}
		*/
			
	
}
