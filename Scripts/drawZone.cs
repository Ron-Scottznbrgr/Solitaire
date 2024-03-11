using Godot;
using System;


public partial class drawZone : cardZone
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
		cardList.ForEach( c => { c.Call("SetSelectable", false);});
		cardList.Add(card);
		card.Call("RevealCard",this.GlobalPosition);
		card.Call("SetCurrentZone",this);
		card.Call("SetPreviousPosition",this.GlobalPosition);

		/*
		GD.Print(""+this.Name + " is accepting a card...");
		foreach(Node cardInList in cardList)
		{
			//cardInList.Call("DebugPrintCardToConsole");
		}		*/

		ReorderCards();	
	}

	public override bool RuleCheck(Card card)
	{
		return false;

	}	
	

/*
	public void ReturnAllCards(Node deckZone)
	{
		ReorderCards();
		foreach(Node cardInList in cardList)
		{
			
	{
			cardInList.Call("SetZIndex",cardZindex);
			cardZindex+=1;
		}	
		}
		*/
	
}
