using Godot;
using System;

public partial class Player : Node2D
{

	private float inputDelay=0.2f;	//Time before player can press the draw button
	private float inputTimer=0.0f;	//Timer to indicate when the player can draw
	private Boolean canInput = true;	//If timer > delay, canInput
	private Node table;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		GetReferences();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	if (canInput)
	{
		if (Input.IsActionJustPressed("rs_draw"))
		{
			//If they can input, reveal the card, and lock input temporarily.
			canInput=false;
			
			table.Call("DrawCard");
			}
		}

		if (Input.IsActionJustPressed("click")){
			Global.playerHoldingMouse = true;
		}
		else if (Input.IsActionJustReleased("click")){
			Global.playerHoldingMouse = false;
		}


		if (Input.IsActionJustPressed("rs_ace1"))
		{
			canInput=false;
			table.Call("MoveCardtoZone",0);
		}
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

	
  	private void GetReferences()
    {
		table = GetNode<Node>("../Table");		
    }

	

}
