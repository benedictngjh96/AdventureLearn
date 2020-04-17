using Godot;
using System;
using System.Collections.Generic;
public class Credits : Node2D
{
    VBoxContainer vbox;
    DynamicFont dFont;
    GridContainer gridContainer;
    List<string> creditsList;
    public override void _Ready()
    {
        vbox = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");
        gridContainer = GetNode<GridContainer>("ScrollContainer/VBoxContainer/GridContainer");
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 19;

        addCreditsList();
        printCreditsList();
    }
    private void addCreditsList()
    {
        creditsList = new List<string>();

        creditsList.Add("Battle Theme A by cynicmusic licensed CC0: https://opengameart.org/content/battle-theme-a");
        creditsList.Add("The Adventure Begins by bart licensed CC-BY 3.0: https://opengameart.org/content/adventure-begins");
        creditsList.Add("SCI-FI EFFECTS by Skorpio licensed CC-BY 3.0 : https://opengameart.org/content/sci-fi-effects");
        creditsList.Add("PAINTERLY SPELL ICONS PART 1 by J.W Bjerk CC-BY 3.0: https://opengameart.org/content/painterly-spell-icons-part-1");
        creditsList.Add("FANTASY SOUND EFFECTS LIBRARY by Little Robot Sound Factory CC-BY 3.0: https://opengameart.org/content/fantasy-sound-effects-library");
    }
    private void printCreditsList()
    {
        foreach (string credit in creditsList)
        {
            Label name = new Label();
            name.AddFontOverride("font", dFont);
            name.Text = credit;
            name.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(name);
        }
    }
}
