using Godot;
using System;

public class BackButton : Node2D
{
    private void _on_TextureButton_pressed()
    {
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
    }

}


