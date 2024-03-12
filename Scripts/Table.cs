using Godot;
using System;
using System.Runtime.ExceptionServices;

public partial class Table : Node2D
{

	PackedScene deckPrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/deck.tscn");
	PackedScene kingPrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/kingZone.tscn");
	PackedScene acePrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/aceZone.tscn");

	PackedScene mouseZonePrefab = (PackedScene)ResourceLoader.Load("res://Assets/Prefabs/mouseZone.tscn");





	private Node[] aceZones = new Node[4];
	private Node[] kingZones = new Node[7];
	private Node mouseZone;
	private Node player;

	private Node drawPile;
	private Node deck;
	private String lastCol="";

	int deckC,drawC=0;
	int ace1C,ace2C,ace3C,ace4C=0;
	int king1C,king2C,king3C,king4C,king5C,king6C,king7C=0;




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		
		SpawnMouseZone();
		SpawnDeck();
		SpawnAceZones();
		SpawnKingZones();
		
		GetReferences();

		DealFaceDown();

		
	}

 

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		
		//GD.Print("Collisions: De:"+deckC+ "  Dr:"+drawC+ "  A1:"+ace1C+"  A2:"+ace2C+"  A3:"+ace3C+"  A4:"+ace4C+"  K1:"+king1C+"  K2:"+king2C+"  K3:"+king3C+"  K4:"+king4C+"  K5:"+king5C+"  K6:"+king6C+"  K7:"+king7C);
	}

   	private void GetReferences()
    {
		deck = GetNode<Node>("Deck");
		drawPile = GetNode<Node>("Deck/drawZone");
		player = GetNode<Node>("../Player");
		
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

	public void SpawnMouseZone()
	{
		Node myNewZone;
		   // Instance the "prefab" card to create a new node
		   //Assign that node to the myNewCard variable.
		   	myNewZone = null;
            myNewZone = mouseZonePrefab.Instantiate();

            // Add the new node as a child of the current scene or the root node
			//GD.Print ("Setting Ace Zone to: "+(new Vector2 (acePosX + XposOffset, acePosY)));
			myNewZone.Call("SetPos",new Vector2(0,0));
			mouseZone = myNewZone;
			AddChild(myNewZone,true);
	}

	public void DealFaceDown()
	{
		//GD.Print("Attempting to deal face down");
		deck.Call("MoveCardtoZone",kingZones[0]);

		deck.Call("MoveCardtoZone",kingZones[1]);
		deck.Call("MoveCardtoZone",kingZones[1]);				

		deck.Call("MoveCardtoZone",kingZones[2]);		
		deck.Call("MoveCardtoZone",kingZones[2]);
		deck.Call("MoveCardtoZone",kingZones[2]);

		deck.Call("MoveCardtoZone",kingZones[3]);
		deck.Call("MoveCardtoZone",kingZones[3]);
		deck.Call("MoveCardtoZone",kingZones[3]);

		deck.Call("MoveCardtoZone",kingZones[3]);
		

		deck.Call("MoveCardtoZone",kingZones[4]);
		deck.Call("MoveCardtoZone",kingZones[4]);
		deck.Call("MoveCardtoZone",kingZones[4]);

		deck.Call("MoveCardtoZone",kingZones[4]);
		deck.Call("MoveCardtoZone",kingZones[4]);


		deck.Call("MoveCardtoZone",kingZones[5]);
		deck.Call("MoveCardtoZone",kingZones[5]);
		deck.Call("MoveCardtoZone",kingZones[5]);

		deck.Call("MoveCardtoZone",kingZones[5]);
		deck.Call("MoveCardtoZone",kingZones[5]);
		deck.Call("MoveCardtoZone",kingZones[5]);


		deck.Call("MoveCardtoZone",kingZones[6]);
		deck.Call("MoveCardtoZone",kingZones[6]);
		deck.Call("MoveCardtoZone",kingZones[6]);

		deck.Call("MoveCardtoZone",kingZones[6]);
		deck.Call("MoveCardtoZone",kingZones[6]);
		deck.Call("MoveCardtoZone",kingZones[6]);

		deck.Call("MoveCardtoZone",kingZones[6]);
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

	public void MoveCardtoZone(int zone, int kingCardPile)
	{
		GD.Print("----------------------------------------------------------------------------------");
		GD.Print("Zone = "+zone +"    KKP =" +kingCardPile);
		Node targetZone = null;
		Node kingPile = null;

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

			case 88:
			targetZone = drawPile;
			break;
			
			case 99:
			player.Call("MouseFull",true);
			targetZone = mouseZone;
			break;

		}

		switch (kingCardPile)
		{
			case 0:
			kingPile = null;
			break;
			
			case 4:
			kingPile = kingZones[0];
			break;

			case 5:
			kingPile = kingZones[1];
			break;
			
			case 6:
			kingPile = kingZones[2];
			break;
			
			case 7:
			kingPile = kingZones[3];
			break;
			
			case 8:
			kingPile = kingZones[4];
			break;
			
			case 9:
			kingPile = kingZones[5];
			break;
			
			case 10:
			kingPile = kingZones[6];
			break;

		}

		

		

		Node2D targetZone2D;
		targetZone2D = (Node2D)targetZone;

		//if ((bool)targetZone.Call("HasCards"))
		//{
			int topValue;
			int topSuit;
			//Moves card from the draw pile to the mouse.
			if (targetZone==mouseZone && kingCardPile<4)
			{
				if ((bool)drawPile.Call("HasCards"))drawPile.Call("MoveCardtoZone",targetZone);
				player.Call("MouseFull",true);	
			}
			else if (kingCardPile>=4)
			{
				//This lets you take cards out of the king's piles and move them
				if (kingPile !=null)
				{
					//GD.Print("Testing Mouserrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr");
				if ((bool)kingPile.Call("HasCards"))
				{
					if ((bool)kingPile.Call("IsTopCardRevealed"))
					{
						GD.Print("The Top card is already revealed");
					kingPile.Call("MoveCardtoZone",mouseZone);	
					player.Call("MouseFull",true);
					}
					else
					{
						GD.Print("The Top card is NOT revealed, but now is.");
						kingPile.Call("RevealTopCard");
						player.Call("MouseFull",false);
					}
				}
				
				}
			}
			else if (targetZone==drawPile)
			{
				//Move card to draw pile..
				//GD.Print("Going back to draw");
				mouseZone.Call("MoveCardtoZone",drawPile,true);
					player.Call("MouseFull",false);	
			}
			else
			{
				if ((bool)targetZone.Call("HasCards"))
				{			
					//Moving cards to the King's Piles from the mouse
					//GD.Print("999999999999999999999999999999999999999999999999999999999999999999999999999999");	
					GD.Print("KINGS CARD PILE = "+kingCardPile);	
					topSuit=(int)targetZone.Call("TopCardSuit");
					topValue=(int)targetZone.Call("TopCardValue");	
					GD.Print ("Target Zone has cards!  "+topSuit+"//"+topValue);
					
					if ((bool)targetZone.Call("RuleCheck",(int)mouseZone.Call("TopCardValue"),(int)mouseZone.Call("TopCardSuit"),topSuit,topValue))
					{
					mouseZone.Call("MoveCardtoZone",targetZone);
					player.Call("MouseFull",false);	
					//drawPile.Call("MoveCardtoZone",targetZone);	
					}			
				}
				else
				{
					//Moving cards to the Zones if there are no cards in them
					//GD.Print("8888888888888888888888888888888888888888888888888888888888888888888888888888888");
					GD.Print (""+targetZone.Name+" has NO cards!");
					if ((bool)targetZone.Call("RuleCheck",(int)mouseZone.Call("TopCardValue")))
					{
						mouseZone.Call("MoveCardtoZone",targetZone);
						player.Call("MouseFull",false);
						//drawPile.Call("MoveCardtoZone",targetZone);
					}				
				}
			}
		//}
	}


	public void GetMouseCol(String colName)
	{
		//This is super sloppy. I am not a fan of this, but this is the quickest way I can think of doing this. Sad Face :(
		if (colName=="Deck")
		{
			deckC=1;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "drawZone")
		{
			deckC=0;
			drawC=1;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "aceZone")
		{
			deckC=0;
			drawC=0;

			ace1C=1;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "aceZone2")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=1;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "aceZone3")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=1;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "aceZone4")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=1;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "kingZone")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=1;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "kingZone2")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=1;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "kingZone3")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=1;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "kingZone4")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=1;
			king5C=0;
			king6C=0;
			king7C=0;
		}
		else if (colName == "kingZone5")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=1;
			king6C=0;
			king7C=0;
		}
		else if (colName == "kingZone6")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=1;
			king7C=0;
		}
		else if (colName == "kingZone7")
		{
			deckC=0;
			drawC=0;

			ace1C=0;
			ace2C=0;
			ace3C=0;
			ace4C=0;

			king1C=0;
			king2C=0;
			king3C=0;
			king4C=0;
			king5C=0;
			king6C=0;
			king7C=1;
		}
		else
		{
			if (colName=="NotDeck")deckC=0;
			if (colName=="NotdrawZone")drawC=0;

			if (colName=="NotaceZone")ace1C=0;
			if (colName=="NotaceZone2")ace2C=0;
			if (colName=="NotaceZone3")ace3C=0;
			if (colName=="NotaceZone4")ace4C=0;

			if (colName=="NotkingZone")king1C=0;
			if (colName=="NotkingZone2")king2C=0;
			if (colName=="NotkingZone3")king3C=0;
			if (colName=="NotkingZone4")king4C=0;
			if (colName=="NotkingZone5")king5C=0;
			if (colName=="NotkingZone6")king6C=0;
			if (colName=="NotkingZone7")king7C=0;
		}
		
		//Sending Collision Data to Player...

		if (deckC==1)player.Call("CollidingWith","deck");
		else if (drawC==1)player.Call("CollidingWith","drawZone");
		
		else if (ace1C==1)player.Call("CollidingWith","aceZone");
		else if (ace2C==1)player.Call("CollidingWith","aceZone2");
		else if (ace3C==1)player.Call("CollidingWith","aceZone3");
		else if (ace4C==1)player.Call("CollidingWith","aceZone4");

		else if (king1C==1)player.Call("CollidingWith","kingZone");
		else if (king2C==1)player.Call("CollidingWith","kingZone2");
		else if (king3C==1)player.Call("CollidingWith","kingZone3");
		else if (king4C==1)player.Call("CollidingWith","kingZone4");
		else if (king5C==1)player.Call("CollidingWith","kingZone5");
		else if (king6C==1)player.Call("CollidingWith","kingZone6");
		else if (king7C==1)player.Call("CollidingWith","kingZone7");
		else player.Call("CollidingWith","null");
		



	}

	
}
