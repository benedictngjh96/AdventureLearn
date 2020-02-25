using Godot;
using System;

public class MainMenu : Node2D
{

    public override void _Ready()
    {

    }
    private void _on_WorldBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/World/World.tscn");
    }

}
