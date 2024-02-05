using Godot;
using System;
using System.Collections.Generic;

public partial class deck : Node2D
{
	PackedScene cardPrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/card.tscn");
	
	//public int[,] cards = new int [52,2];
	//Decided against an array... Using a list of Nodes instead.
	public List<Node> cardList = new List<Node>(52);
	
	public int displayX=0;	//Code to configure simply X/Y for display of cards
	public int displayY=0;	//Will become obsolete as we find something to do with the cards
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GD.Print("Hello World");	-> Debug Test. Prints "Hello World" to the console
		
		//Creates Cards, and assigns them to the deck
		CreateDeck();
		
		//Suffles the cards.
		ShuffleCards();
		
		//Displays the card in a grid on the screen. 4 rows of 13.
		DisplayCards();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void CreateDeck()
	{
		//This short loop makes assigns the card values / suits to new cards
		for (int suit = 0; suit < 4; suit++)
			{
			for (int value = 1; value<14; value++)
			{	
		
				//Creates a card... 
				CreateCard(value, suit);
			}
		}
	}


	public void CreateCard(int value, int suit)
	{
		   // Instance the "prefab" card to create a new node
		   //Assign that node to the myNewCard variable.
			Node myNewCard = cardPrefab.Instantiate();

			// Add the new node as a child of the current scene or the root node
			AddChild(myNewCard,true);

			//Call a method from the card class... 
			//Setting the card's data with value and suit.
			myNewCard.Call("SetCardData", value, suit);
			
			//Another example of debug text.
			//GD.Print(value + " // "+suit);
			
			//Adding the new card to the cardList.
			cardList.Add(myNewCard);
	}

	public void ShuffleCards()
	{
		//Setting up random nums.
		Random random = new Random();
		
		//Node to temporarily hold a card while we move it in the list.
		Node prevCard;
		
		//just to hold the rand num
		int num;
		
		//Small loop to shuffle. 
		//Takes a card randomly from the deck.
		//Puts it on top of the deck.
		//Repeat 1000 times.

		for (int i=0; i<1000; i++)
		{
		//Rand from 0 to the amount of cards in the list (52)		
		num = random.Next(0,cardList.Count);
		
		//copying the card picked to prevCard
		prevCard = cardList[num];

		//Removing the card from the list
		cardList.RemoveAt(num);

		//Adding the card back to the top of the list.
		cardList.Add(prevCard);
		}
	}


	public void DisplayCards()
	{
		//For each Node (card) in the list...
		foreach(Node cardInList in cardList)
		{
			
			//Var for display positioning.
			displayX+=1;
			
			//if row is full... go to next row, reset x pos.
			if (displayX==14)
			{
				displayX=1;
				displayY+=1;			
			}
			
			//set x/y to vector, pass to card to set position.
			Vector2 pos = new Vector2(displayX*50,displayY*100);
			cardInList.Call("SetCardPos", pos); 
		}

		//This is for more of a Godot thing... 
		//By default it draws the objects in order they appear in the scene tree...
		//If we want the closer cards to be drawn on top of the cards in back,
		//we can reverse the list, order them on the tree, then reverse the list back.
		//
		//We can do this in other ways, I was just being lazy.
		cardList.Reverse();

		//for each card...
		for (int i=0; i<cardList.Count; i++)
		{
			MoveChild(cardList[i],0);
		}
		
		//reverse the list back.
		cardList.Reverse();		
	}
}
