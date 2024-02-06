using Godot;
using System;
using System.Collections.Generic;

// Can inherit this class to add rules/functionality
public partial class PlayArea : Node2D
{
    // List of current cards in the play area
	public List<Node> cardList = new List<Node>();

    // Position that a played card snaps to
    [Export]
    public Vector2 playPosition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    // Override this function when inheriting
    // Used to check game logic when attempting to move a card to the play area
    public bool PlaceInPlayArea(Node card)
    {
        //Game logic goes here
        AddCard(card);
        return true;
    }
    public void AddCard(Node card)
    {
        cardList.Add(card);
    }
}
