using Godot;
using System;

public partial class Player : Node2D
{

	Vector2 mousePosition;
	String colZone="";

	private float inputDelay=0.2f;	//Time before player can press the draw button
	private float inputTimer=0.0f;	//Timer to indicate when the player can draw
	private Boolean canInput = true;	//If timer > delay, canInput
	private Node table;
	private Boolean isMouseFull=false;
	private int lastZone;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		GetReferences();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print("Mouse Full  "+isMouseFull);
		//GD.Print("Can Input  "+canInput);
		mousePosition = GetGlobalMousePosition();

	if (canInput)
	{
		if (Input.IsActionJustPressed("rs_draw"))
		{
			//If they can input, reveal the card, and lock input temporarily.
			canInput=false;
			
			table.Call("DrawCard");
			}
		}


        if (Input.IsActionJustPressed("rs_click") && (colZone == "deck"))
		{

			//If they can input, reveal the card, and lock input temporarily.
			canInput=false;
			
			table.Call("DrawCard");		

		}

		if (Input.IsActionJustPressed("rs_click") && (colZone == "drawZone") && isMouseFull==false)
		{
			isMouseFull=true;
			canInput=false;
			table.Call("MoveCardtoZone",99,0);
		}

		if (Input.IsActionJustPressed("rs_r_click") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",88,0);
			//table.Call("MoveCardtoZone",lastZone);			
		}
/*
		if (Input.IsActionJustPressed("rs_click") && (colZone == "drawZone") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",lastZone,0);
			//table.Call("MoveCardtoZone",lastZone);			
		}*/

		if (Input.IsActionJustPressed("rs_click") && (colZone == "aceZone") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",0,0);
			
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "aceZone2") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",1,0);

		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "aceZone3") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",2,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "aceZone4") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",3,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",4,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone2") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",5,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone3") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",6,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone4") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",7,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone5") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",8,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone6") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",9,0);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone7") && isMouseFull)
		{
			canInput=false;
			table.Call("MoveCardtoZone",10,0);
		}







		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,4);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone2") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,5);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone3") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,6);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone4") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,7);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone5") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,8);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone6") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,9);
		}
		if (Input.IsActionJustPressed("rs_click") && (colZone == "kingZone7") && isMouseFull==false && canInput)
		{
			canInput=false;
			table.Call("MoveCardtoZone",99,10);
		}

		if (Input.IsActionJustPressed("add_score"))
		{
			Node scoreLabel = GetNode<Node>("../UI/ScoreLabel");
			scoreLabel.Call("OnCardMoveToAceZone");	
		}
		


if (Input.IsActionJustReleased("rs_click") && canInput==false)
{
	canInput=true;
}


		if (Input.IsActionJustPressed("rs_ace1"))
		{
			GD.Print("Mouse Full  "+isMouseFull);
			GD.Print("Can Input  "+canInput);
		}



		if (Input.IsActionJustPressed("rs_ace2"))
		{
			canInput=false;
				table.Call("DealFaceDown");
		}
		/*
		if (Input.IsActionJustPressed("rs_ace2"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",1);
		}
		if (Input.IsActionJustPressed("rs_ace3"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",2);
		}
		if (Input.IsActionJustPressed("rs_ace4"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",3);
		}

		if (Input.IsActionJustPressed("rs_king1"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",4);
		}
			if (Input.IsActionJustPressed("rs_king2"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",5);
		}
			if (Input.IsActionJustPressed("rs_king3"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",6);
		}
			if (Input.IsActionJustPressed("rs_king4"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",7);
		}
			if (Input.IsActionJustPressed("rs_king5"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",8);
		}
			if (Input.IsActionJustPressed("rs_king6"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",9);
		}
			if (Input.IsActionJustPressed("rs_king7"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",10);
		}
*/
		
		//If you can't input... Start a timer, and reset input
		/*
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
		}*/
	}	

	public void CollidingWith(String mouseZone)
	{
				colZone = mouseZone;
	}

	public void MouseFull(Boolean mouseFull)
	{
				isMouseFull= mouseFull;
	}


	
  	private void GetReferences()
    {
		table = GetNode<Node>("../Table");		
    }


	

}
