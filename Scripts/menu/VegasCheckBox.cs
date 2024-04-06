using Godot;
using System;

public partial class VegasCheckBox : CheckBox
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_toggled(bool toggled_on)
	{
		GD.Print("Toggled Vegas mode to: " + toggled_on);
		var globalVar = GetNode<global_variables>("/root/GlobalVariables");
		globalVar.isVegasMode = toggled_on;
	}
}
