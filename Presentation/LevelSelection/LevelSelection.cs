using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelSelection : Node2D
{
    SectionBL sectionBL;
    Section section;
    List<Section> sectionList;
    Sprite forwardSprite;
    TextureButton forwardBtn;
    Sprite backwardSprite;
    TextureButton backWardBtn;
    Label sectionLbl;
    int count = 1;

    public override void _Ready()
    {
        //Intializing nodes
        sectionBL = new SectionBL();
        section = new Section();
        forwardSprite = GetNode<Sprite>("ArrowNavigation/Forward"); 
        forwardBtn = GetNode<TextureButton>("ArrowNavigation/Forward/ForwardBtn");
        backwardSprite = GetNode<Sprite>("ArrowNavigation/Backward");
        backWardBtn = GetNode<TextureButton>("ArrowNavigation/Backward/BackwardBtn");
        sectionLbl = GetNode<Label>("Title/SectionName");

        //REMOVE
        Global.worldId = 1;
        //On load set section id to 1 to display section 1
        Global.sectionId = 1;
        sectionList = sectionBL.GetWorldSections();
        sectionLbl.Text = sectionList[count - 1].SectionName;

        DisplaySectionLevels();
        //Disable forward n back btns if only have 1 level
        DisableBothButtons();
        DisableBackwardButton();
    }
    private void _on_ForwardBtn_pressed()
    {
        count++;
        if (count >= sectionList.Count())
            DisableForwardButton();
        if (backWardBtn.Disabled == true)
            EnableBackwardButton();
        DisplaySectionName();
    }
    private void _on_BackwardBtn_pressed()
    {
        count--;
        if (count <= 1)
            DisableBackwardButton();
        if (forwardBtn.Disabled == true)
            EnableForwardButton();
        DisplaySectionName();
    }
    private void DisplaySectionName()
    {
        sectionLbl.Text = sectionList[count - 1].SectionName;
    }
    private void EnableForwardButton()
    {
        forwardSprite.Modulate = new Color(255, 255, 255);
        forwardBtn.Disabled = false;
    }
    private void EnableBackwardButton()
    {
        backwardSprite.Modulate = new Color(255, 255, 255);
        backWardBtn.Disabled = false;
    }
    private void DisableForwardButton()
    {
        forwardSprite.Modulate = new Color(0, 0, 0);
        forwardBtn.Disabled = true;
    }
    private void DisableBackwardButton()
    {
        backwardSprite.Modulate = new Color(0, 0, 0);
        backWardBtn.Disabled = true;
    }
    private void DisableBothButtons()
    {
        if(sectionList.Count == 1)
        {
            DisableForwardButton();
            DisableBackwardButton();
        }
    }
    private void DisplaySectionLevels()
    {
        section = sectionBL.DisplaySectionLevels();
        string levelPath;
        Sprite levelNode;
        int count = 1;
        foreach(Level level in section.Level)
        {
            levelPath = String.Format("Levels/Level{0}", count);
            levelNode = GetNode<Sprite>(levelPath);
            levelNode.SetVisible(true);
            count++;
        }
    }

}
