using Godot;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Class to handle Presentation for Credits
/// </summary>
public class Credits : Node2D
{
    VBoxContainer vbox;
    DynamicFont dFont;
    GridContainer gridContainer;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        vbox = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");
        gridContainer = GetNode<GridContainer>("ScrollContainer/VBoxContainer/GridContainer");
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 19;
        DisplayCredits();
    }

    /// <summary>
    /// Display the Credits
    /// </summary>
    private void DisplayCredits()
    {
        Godot.File file = new Godot.File();
        file.Open("res://Credits/credits.txt", Godot.File.ModeFlags.Read);
        string content = file.GetAsText();
        file.Close();
        Label name = new Label();
        name.AddFontOverride("font", dFont);
        name.Text = content;
        name.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(name);

    }
}
