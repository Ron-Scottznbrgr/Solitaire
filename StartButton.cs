using Godot;
using System;

public partial class StartButton : Button
{
 public void _on_StartGame_pressed()
   {
	   GetTree().ChangeScene("res://path/to/YourGameScene.tscn");
   }

   public void _on_Quit_pressed()
   {
	   GetTree().Quit();
   }
