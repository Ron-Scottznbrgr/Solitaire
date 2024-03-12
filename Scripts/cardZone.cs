using Godot;
using System;
using System.Collections.Generic;
using System.Net.Security;

public partial class cardZone : Node2D
{

	Vector2 mousePosition;

	public List<Node> cardList = new List<Node>(52);
	public CollisionShape2D colBox;
	public Node2D table;
	Boolean isMouseInside=false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetColBox();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	 
		IsMouseCol();
		ReorderCards();
	}


	public void SetPos(Vector2 newPos)
	{
		this.GlobalPosition = newPos;
		
		//GD.Print(newPos + "   //////   drawpile          positions");		
	}

	public virtual void GetColBox()
	{
		table = GetNode<Node2D>("../../Table");
		colBox = GetNode<CollisionShape2D>("Body/BodyCol");
		GD.Print("My Name is "+this.Name);
		GD.Print("My body is called"+colBox.Name);
	}

	public void IsMouseCol()
	{
		// Check if the CollisionShape2D is available
        if (colBox != null)
        {
            // Get the rectangle in local coordinates
            Rect2 localRect = colBox.Shape.GetRect();

            // Get the global position of the CollisionShape2D
            Vector2 globalPosition = colBox.GlobalPosition;

            // Adjust the rectangle to global coordinates
            Rect2 globalRect = new Rect2(globalPosition, localRect.Size);			
			
			// Get the mouse position in global coordinates
            mousePosition = GetGlobalMousePosition();

            // Check if the mouse position is within the rectangle
            if (globalRect.HasPoint(mousePosition))
            {
				if (isMouseInside==false)
				{
				isMouseInside=true;
				//	GD.Print(globalRect.Position," ",globalRect.End);
                GD.Print("Mouse is inside "+this.Name);
				table.Call("GetMouseCol",this.Name);
				//GD.Print(mousePosition);
				}			
            }
            else
            {
				isMouseInside=false;
				table.Call("GetMouseCol","Not"+this.Name);
				//GD.Print(globalRect.Position," ",globalRect.End);
                //GD.Print("Mouse is outside the rectangle.");
				//GD.Print(mousePosition);
            }
        }
	}



	public virtual void CardIntake(Node card)
	{
		cardList.Add(card);
		ReorderCards();	
		card.Call("ZoneTransfer",this);

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
		int topCard=cardList.Count;
		GD.Print(""+this.Name + ", which has "+topCard+" cards in it, is removing a card...");
		
		//cardList[topCard-1].Call("SetZIndex",10);
		cardList.RemoveAt(topCard-1);
	}


	public virtual void MoveCardtoZone(Node2D targetZone)
	{
		
		int topCard=cardList.Count;

		if (cardList.Count > 0)
		{
			GD.Print("Trying to remove a card from "+this.Name);
			//cardList[topCard-1].Call("DebugPrintCardToConsole");
			targetZone.Call("HasCards");
			targetZone.Call("CardIntake",cardList[topCard-1]);
			targetZone.Call("HasCards");
			CardOuttake();
			this.Call("HasCards");
			GD.Print(""+this.Name+" is Moving a card to Zone "+targetZone.Name);
		}
	}

	public virtual void MoveCardtoZone(Node2D targetZone, bool drawZoneEmpty)
	{
		int topCard=cardList.Count;
		GD.Print("Cards in "+this.Name+ "  = "+topCard);
		if (cardList.Count > 0)
		{
			foreach(Node cardInList in cardList)
			{
			GD.Print("Looping");
			cardInList.Call("FlipCard");

			//GD.Print("Trying to remove a card from "+this.Name);
			//cardList[topCard-1].Call("DebugPrintCardToConsole");
			//targetZone.Call("HasCards");
			targetZone.Call("CardIntake",cardInList);
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
		
		
		foreach(Node cardInList in cardList)
		{
			cardInList.Call("SetZIndex",cardZindex);
			cardZindex+=1;
		}	
		
	}

	
	public bool HasCards()
	{
		if (cardList.Count>0)
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
		int topCard=cardList.Count;
		int suit=-999;

		if (cardList.Count > 0)
		{
			suit = (int)cardList[topCard-1].Call("GetCardSuit");
		}
		GD.Print(this.Name + "card count is currently "+topCard);
		GD.Print(this.Name+"top suit is "+suit);
		return suit;
	}

	public virtual int TopCardValue()
	{
		int topCard=cardList.Count;
		int value=-999;

		if (cardList.Count > 0)
		{
			value = (int)cardList[topCard-1].Call("GetCardValue");
		}
		GD.Print(this.Name + "card count is currently "+topCard);
		GD.Print(this.Name+"top value is "+value);
		return value;
	}

	public void ClearZone()
	{
	cardList.Clear();
	}

	public void MouseCollide()
	{

	}


}
