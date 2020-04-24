using Godot;
using System;

/// <summary>
/// Class to handle Presentation for BackButton
/// </summary>
public class BackButton : Node2D
{
    /// <summary>
    /// Change scene to MainMenu.tscn whenever the Back button is pressed
    /// </summary>
    private void _on_TextureButton_pressed()
    {
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
    }

}


