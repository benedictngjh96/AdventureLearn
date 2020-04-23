using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class to handle Presentation for WorldScreen
/// </summary>
public class WorldScreen : Node
{
    TextureButton world1Btn, world2Btn, world3Btn; 
    WorldBL worldBl;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        world1Btn = GetNode<TextureButton>("Bg/World1");
        world2Btn = GetNode<TextureButton>("Bg/World2");
        world3Btn = GetNode<TextureButton>("Bg/World3");
        worldBl = new WorldBL();
        DisableInaccessibleWorlds();
    }

    /// <summary>
    /// Disable the Worlds that have not been unlocked
    /// </summary>
    private void DisableInaccessibleWorlds()
    {
        int completedWorldCount = worldBl.GetCompletedWorldCount();
        int totalWorldCount = worldBl.GetTotalWorldCount();

        for (int i = totalWorldCount; i > completedWorldCount + 1 ; i--)
        {
            TextureButton disabler = GetNode<TextureButton>("Bg/World" + i);
            disabler.Disabled = true;
        }
    }

    /// <summary>
    /// Handles the logic whenever the World1's button is pressed
    /// </summary>
    private void _on_World1_pressed()
    {
        Global.WorldId = 1;
        GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
    }

    /// <summary>
    /// Handles the logic whenever the World2's button is pressed
    /// </summary>
    private void _on_World2_pressed()
    {
        Global.WorldId = 2;
        GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
    }

    /// <summary>
    /// Handles the logic whenever the World3's button is pressed
    /// </summary>
    private void _on_World3_pressed()
    {
        Global.WorldId = 3;
        GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
    }
}







