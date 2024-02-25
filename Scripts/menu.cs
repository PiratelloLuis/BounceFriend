using System.Runtime.CompilerServices;
using Godot;


public partial class menu : Control
{
    public override void _Ready()
    {

    }

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
	public void _on_color_right_pressed(){
		
	}
	public void _on_color_left_pressed(){

	}

	public void _on_eye_left_pressed(){

	}
	public void _on_eye_right_pressed(){

	}
}
