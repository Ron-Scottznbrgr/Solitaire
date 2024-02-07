using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class deck : Node2D
{
	PackedScene cardPrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/card.tscn");
	
	//public int[,] cards = new int [52,2];
	//Decided against an array... Using a list of Nodes instead.
	public List<Node> cardList = new List<Node>(52);
	
	private int displayX=60;	//Code to configure simply X/Y for display of cards
	private int displayY=70;	//Will become obsolete as we find something to do with the cards
	private float inputDelay=0.2f;	//Time before player can press the draw button
	private float inputTimer=0.0f;	//Timer to indicate when the player can draw
	private Boolean canInput = true;	//If timer > delay, canInput

	public ColorRect cardCounterRect; 	// Box to put Text on to Display Num of Cards.
	public Label cardCountLabel;		//Counter for cards in Deck.

	private Vector2 revealPilePosition= new Vector2 (800,170);
	private int cardCountInt;			//Just holds a int with the card count.
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


		//GD.Print("Hello World");	-> Debug Test. Prints "Hello World" to the console
		
		//Creates Cards, and assigns them to the deck
		CreateDeck();
		
		//Suffles the cards.
		ShuffleCards();
		
		//Create a Deck Pile
		CreateDeckPile();

		//Reference for the Card Counter ColorRect
		cardCounterRect = GetNode<ColorRect>("CountRect");
		cardCounterRect.GlobalPosition = new Vector2(displayX-20,displayY-(cardCounterRect.Size.Y+25));

		//Reference for the Card Counter ColorRect
		cardCountLabel = GetNode<Label>("CountLabel");
		cardCountLabel.GlobalPosition = new Vector2(displayX-20,displayY-(cardCounterRect.Size.Y+18));
		

		//Displays the card in a grid on the screen. 4 rows of 13. //Debug Only
		//DisplayCards();

		//Reveal Top Card of Deck
		//RevealTopCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Updates the label with amount of cards in the deck...
		//This should probably go somewhere else, but until I find a more reliable location, here it is.
		//Does it need to update every frame? No. But here we are anyways 	¯\_(ツ)_/¯
		cardCountInt = cardList.Count; 
		cardCountLabel.Text = ("   Card Count: "+cardCountInt);

		//If the player presses the "UP" button (arrow keys), draw a card.
		if (Input.IsActionPressed("ui_up"))
		{
			//If they can input, reveal the card, and lock input temporarily.
			if (canInput)
			{
			canInput=false;
			RevealTopCard();
			}
		}

		
		//If you can't input... Start a timer, and reset input
		if (!canInput)
		{
			if (inputTimer<= inputDelay)
			{
				inputTimer+=(float)(1*delta);
				//GD.Print(inputTimer + "   ////   "+inputDelay);
			}
			else
			{
				//reset timer to 0 when input is unlocked
				canInput = true;
				inputTimer=0.0f;
			}
		}
		

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


	public void CreateDeckPile()
	{
		int cardCounter=0;
		//int loopCounter=0;	//debug stuff, counts the cards as they print data to console

		//For each Node (card) in the list...
		foreach(Node cardInList in cardList)
		{
			
			if (cardCounter==4)
			{		
			//Var for display positioning.
			displayX+=2;		//Negative Values put 
			displayY+=2;		//Negative Values put Deck to Right, Pos = Left
			cardCounter=0;
			}
			cardCounter+=1;
			//loopCounter+=1;
		
			//set x/y to vector, pass to card to set position.
			Vector2 pos = new Vector2(displayX,displayY);

			//Sets current position in deck AND the reveal pile position for later use.
			//Sends over displayX and Y to adjust offset of draw pile. Makes it look good. May remove later.
			cardInList.Call("SetCardPosition", pos, pos+revealPilePosition, displayX,displayY); 
			//GD.Print("Card Counter: "+loopCounter);
			//cardInList.Call("DebugPrintCardToConsole");	//calls debug card data


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

		/*
		// Top and Bottom of Deck Debug
		GD.Print("BOTTOM of Deck: ");
		cardList[0].Call("DebugPrintCardToConsole");

		GD.Print("TOP of Deck: ");
		cardList[51].Call("DebugPrintCardToConsole");
		*/

	}


	public void RevealTopCard()
	{
		int topCard=cardList.Count-1;

		if (topCard>=0)
		{	
		//Calls the card in list's method to Reveal it... The reveal method moves it as well, and puts it in the "DrawPile" or whatever it's called.
		cardList[topCard].Call("RevealCard");
		//More drawing order shenanigans... Don't know if it's neccesary or not. Seems to work as is. I'll come back later.
		MoveChild(cardList[topCard],51);
		//Remove card from the deck list... Need to add it to the DrawPile list later....
		cardList.RemoveAt(topCard);
		}
	}


	


	//Debug Method for Displaying Cards;
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
			Vector2 pos = new Vector2((displayX-60)*50,(displayY-70)*100);
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
