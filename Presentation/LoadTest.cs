using Godot;
using System;

public class LoadTest : Node2D
{
    /// <summary>
    /// Initialization
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        
    }

    /// <summary>
    /// Calls the show() method in LoadingScreen class
    /// </summary>
    /// <returns></returns>
    private void _on_Start_pressed()
    {
        LoadingScreen.show();
    }

    /// <summary>
    /// Calls the hide() method in LoadingScreen class
    /// </summary>
    /// <returns></returns>
    private void _on_End_pressed()
    {
        LoadingScreen.hide();
    }


}

