using Godot;
using System;
using System.Runtime.ExceptionServices;

public partial class Table : Node2D
{

	PackedScene deckPrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/deck.tscn");
	PackedScene kingPrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/kingZone.tscn");
	PackedScene acePrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/aceZone.tscn");


	private Node[] aceZones = new Node[4];
	private Node[] kingZones = new Node[7];

	private Node drawPile;
	private Node deck;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		
		SpawnDeck();
		SpawnAceZones();
		SpawnKingZones();


		GetReferences();
	}

 

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

   	private void GetReferences()
    {
		deck = GetNode<Node>("Deck");
		drawPile = GetNode<Node>("Deck/drawZone");
    }

	public void SpawnDeck()
	{
		   // Instance the "prefab" card to create a new node
		   //Assign that node to the myNewCard variable.
            Node myNewDeck = deckPrefab.Instantiate();

            // Add the new node as a child of the current scene or the root node
			AddChild(myNewDeck,true);
			
	}

	public void SpawnAceZones()
	{
		int zonePosX = 676;
		int zonePosY = 150;
		int XposOffset = 0;
		int XposOffsetIncrease = 192;
		Node myNewZone;
		for (int i=0; i<4;i++)
		{
		   // Instance the "prefab" card to create a new node
		   //Assign that node to the myNewCard variable.
		   	myNewZone = null;
            myNewZone = acePrefab.Instantiate();

            // Add the new node as a child of the current scene or the root node
			//GD.Print ("Setting Ace Zone to: "+(new Vector2 (acePosX + XposOffset, acePosY)));
			myNewZone.Call("SetPos",new Vector2(zonePosX + XposOffset, zonePosY));
			aceZones[i]=myNewZone;
			AddChild(myNewZone,true);

			XposOffset += XposOffsetIncrease;

		}
	}

	public void SpawnKingZones()
	{
		int zonePosX = 100;
		int zonePosY = 450;
		int XposOffset = 0;
		int XposOffsetIncrease = 192;
		Node myNewZone;
		
		for (int i=0; i<7;i++)
		{
		   // Instance the "prefab" card to create a new node
		   //Assign that node to the myNewCard variable.
            myNewZone = null;
            myNewZone = kingPrefab.Instantiate();

			//GD.Print ("Setting King Zone to: "+(new Vector2 (zonePosX + XposOffset, zonePosY)));
           myNewZone.Call("SetPos",new Vector2(zonePosX + XposOffset, zonePosY));
		   kingZones[i]=myNewZone;
			AddChild(myNewZone,true);
			XposOffset += XposOffsetIncrease;
			GD.Print(kingZones[i].Name);
		}
		
	}

	public void DrawCard()
	{
		if ((bool)deck.Call("HasCards"))
		{
		deck.Call("RevealTopCard");
		}
		else
		{
			drawPile.Call("MoveCardtoZone",deck,true);
			deck.Call("ReorderCards");
			deck.Call("Reset");
		}

		
	}

	public void MoveCardtoZone(int zone)
	{
		Node targetZone = null;

		switch (zone)
		{
			case 0:
			targetZone = aceZones[0];
			break;
			case 1:
			targetZone = aceZones[1];
			break;
			case 2:
			targetZone = aceZones[2];
			break;
			case 3:
			targetZone = aceZones[3];
			break;

			case 4:
			targetZone = kingZones[0];
			break;
			case 5:
			targetZone = kingZones[1];
			break;
			case 6:
			targetZone = kingZones[2];
			break;
			case 7:
			targetZone = kingZones[3];
			break;
			case 8:
			targetZone = kingZones[4];
			break;
			case 9:
			targetZone = kingZones[5];
			break;
			case 10:
			targetZone = kingZones[6];
			break;

		}

		Node2D targetZone2D;
		targetZone2D = (Node2D)targetZone;

		if ((bool)drawPile.Call("HasCards"))
		{
			int topValue;
			int topSuit;
			
			if ((bool)targetZone.Call("HasCards"))
			{				
				topSuit=(int)targetZone.Call("TopCardSuit");
				topValue=(int)targetZone.Call("TopCardValue");	
				GD.Print ("Target Zone has cards!  "+topSuit+"//"+topValue);
				if ((bool)targetZone.Call("RuleCheck",(int)drawPile.Call("TopCardValue"),(int)drawPile.Call("TopCardSuit"),topSuit,topValue))
				{
				drawPile.Call("MoveCardtoZone",targetZone);	
				}			
			}
			else
			{
				GD.Print ("Target Zone has NO cards!");
				if ((bool)targetZone.Call("RuleCheck",(int)drawPile.Call("TopCardValue")))
				{
					drawPile.Call("MoveCardtoZone",targetZone);
				}				
			}
		}
	}

	
}
