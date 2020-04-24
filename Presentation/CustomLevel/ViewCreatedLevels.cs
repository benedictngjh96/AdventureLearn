using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for ViewCreatedLevels
/// </summary>
public class ViewCreatedLevels : Node2D
{
    VBoxContainer vbox;
    GridContainer gridContainer;
    DynamicFont dFont;
    DynamicFont dFont2;
    List<CustomLevel> customLevelList;
    CustomLevelBL customLevelBL;
    PopupMenu popupMenu;
    string levelName = "";

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        NotificationPopup.DisplayPopup("Deleted Successfully!");
        popupMenu = GetNode<PopupMenu>("PopupMenu");
        vbox = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");
        gridContainer = GetNode<GridContainer>("ScrollContainer/VBoxContainer/GridContainer");
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 26;
        dFont2 = new DynamicFont();
        dFont2.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont2.Size = 18;
        customLevelBL = new CustomLevelBL();
        DisplayHeader();
        DisplayLevels();

    }

    /// <summary>
    /// Display column titles for the Student's CreatedLevels
    /// </summary>
    private void DisplayHeader()
    {
        //Student Name
        Label lbl = new Label();
        lbl.AddFontOverride("font", dFont);
        lbl.Text = "Game Name" + "       ";
        lbl.AddColorOverride("font_color", new Color(0, 0, 0));

        gridContainer.AddChild(lbl);

        Label lbl3 = new Label();
        lbl3.AddFontOverride("font", dFont);
        lbl3.Text = "           ";
        lbl3.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl3);

        Label lbl4 = new Label();
        lbl4.AddFontOverride("font", dFont);
        lbl4.Text = "   ";
        lbl4.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl4);

        Label lbl5 = new Label();
        lbl5.AddFontOverride("font", dFont);
        lbl5.Text = "   ";
        lbl5.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl5);
    }

    /// <summary>
    /// Display the Levels
    /// </summary>
    private void DisplayLevels()
    {
        Theme theme = ResourceLoader.Load("res://Assets/GUI/BtnUI4.tres") as Theme;
        customLevelList = customLevelBL.GetStudentCustomLevel(Global.StudentId);
        foreach (CustomLevel customLevel in customLevelList)
        {
            Label name = new Label();
            name.AddFontOverride("font", dFont2);
            name.Text = customLevel.CustomLevelName + "      ";
            name.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(name);

            Button playBtn = new Button();
            playBtn.RectMinSize = new Vector2(80, 30);
            playBtn.Text = "Play";
            string[] playArr = new string[1] { playBtn.Name };
            Godot.Collections.Array playAr = new Godot.Collections.Array();
            playAr.Add(playBtn);
            playBtn.Name = customLevel.CustomLevelId.ToString();
            playBtn.Connect("pressed", this, "PlayLevel", playAr);
            playBtn.Theme = theme;
            gridContainer.AddChild(playBtn);

            Button btn = new Button();
            btn.RectMinSize = new Vector2(80, 30);
            btn.Text = "Edit";
            string[] arr = new string[1] { btn.Name };
            Godot.Collections.Array ar = new Godot.Collections.Array();
            ar.Add(btn);
            btn.Name = customLevel.CustomLevelName.ToString();
            btn.Connect("pressed", this, "EditLevel", ar);
            btn.Theme = theme;
            gridContainer.AddChild(btn);

            Button btn2 = new Button();
            btn2.RectMinSize = new Vector2(80, 30);
            btn2.Text = "Delete";
            string[] arr2 = new string[1] { btn2.Name };
            Godot.Collections.Array ar2 = new Godot.Collections.Array();
            ar2.Add(btn2);
            btn2.Name = customLevel.CustomLevelName.ToString() + " ";
            btn2.Connect("pressed", this, "DeleteLevel", ar2);
            btn2.Theme = theme;
            gridContainer.AddChild(btn2);
        }

    }
    private void PlayLevel(Button btn)
    {
        Global.CustomLevelId = Convert.ToInt32(btn.Name);
        GetTree().ChangeScene("res://Presentation/CustomLevel/CustomLevel.tscn");
    }
    /// <summary>
    /// Change scene to EditLevelInit when Edit button is pressed
    /// </summary>
    /// <param name="btn"></param>
    private void EditLevel(Button btn)
    {
        CustomLevel cl = customLevelList.Find(item => item.CustomLevelName == btn.Name.Trim());
        Global.CustomLevelId = cl.CustomLevelId;
        GetTree().ChangeScene("res://Presentation/EditLevel/EditLevelInit.tscn");
    }

    /// <summary>
    /// Display a popup to prompt user for confirmation when Delete button is pressed
    /// </summary>
    /// <param name="btn"></param>
    private void DeleteLevel(Button btn)
    {
        popupMenu.Visible = true;
        levelName = btn.Name.Trim();
    }

    /// <summary>
    /// Clear gridContainer
    /// </summary>
    private void ClearGrid()
    {
        foreach (Node child in gridContainer.GetChildren())
            gridContainer.RemoveChild(child);
    }

    /// <summary>
    /// Remove the popup prompt for deletion when the No button is pressed
    /// </summary>
    private void _on_No_pressed()
    {
        popupMenu.Visible = false;
    }

    /// <summary>
    /// Remove the popup prompt for deletion and handle the deletion through businesss logic when the No button is pressed
    /// </summary>
    private void _on_Yes_pressed()
    {
        CustomLevel cl = customLevelList.Find(item => item.CustomLevelName == levelName);
        customLevelBL.DeleteCustomLevel(cl.CustomLevelId);
        ClearGrid();
        DisplayHeader();
        DisplayLevels();
        popupMenu.Visible = false;
        NotificationPopup.DisplayPopup("Deleted Successfully!");
    }

}


