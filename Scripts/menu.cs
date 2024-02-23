using Godot;
using System;

public partial class menu : Control
{

	public override void _Process(double delta)
	{
	}

	private void _on_play_pressed(){
		GetTree().ChangeSceneToFile("res://Scenes/scene.tscn");

	}
	public void _on_quit_pressed(){
		GetTree().Quit();
	}
	public void _on_name_pressed(){
		OS.ShellOpen("https://github.com/PiratelloLuis");
	}
}
