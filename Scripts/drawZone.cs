using Godot;
using System;


public partial class drawZone : cardZone
{

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetColBox();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
	public override void GetColBox()
	{
		table = GetNode<Node2D>("../../../Table");
		colBox = GetNode<CollisionShape2D>("Body/BodyCol");
		GD.Print("My Name is "+this.Name);
		GD.Print("My body is called"+colBox.Name);
	}


	public override void CardIntake(Node card)
	{
		cardList.Add(card);
		card.Call("RevealCard",this.GlobalPosition);
		/*
		GD.Print(""+this.Name + " is accepting a card...");
		foreach(Node cardInList in cardList)
		{
			//cardInList.Call("DebugPrintCardToConsole");
		}		*/

		ReorderCards();	
	}


	public void CardIntake(Node card,Boolean returning)
	{
		cardList.Add(card);
		ReorderCards();	
		card.Call("ZoneTransfer",this);

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
