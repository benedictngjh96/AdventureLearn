using Godot;
using System;
using System.Collections.Generic;
using System.IO;
public class Credits : Node2D
{
    VBoxContainer vbox;
    DynamicFont dFont;
    GridContainer gridContainer;
    public override void _Ready()
    {
        vbox = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");
        gridContainer = GetNode<GridContainer>("ScrollContainer/VBoxContainer/GridContainer");
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 19;

        DisplayCredits();
    }
    private void DisplayCredits()
    {
        using (StreamReader sr = new StreamReader("Credits/credits.txt"))
        {
            // Read the stream to a string, and write the string to the console.
            String line = sr.ReadToEnd();
            Label name = new Label();
            name.AddFontOverride("font", dFont);
            name.Text = line;
            name.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(name);
        }
    }
}
