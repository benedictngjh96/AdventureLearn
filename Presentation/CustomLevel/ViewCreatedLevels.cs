using Godot;
using System;
using System.Collections.Generic;
public class ViewCreatedLevels : Node2D
{
    VBoxContainer vbox;
    GridContainer gridContainer;
    DynamicFont dFont;
    DynamicFont dFont2;
    List<CustomLevel> customLevelList;
    CustomLevelBL customLevelBL;
    public override void _Ready()
    {
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
    }
    private void DisplayLevels()
    {
        customLevelList = customLevelBL.GetStudentCustomLevel(Global.StudentId);
        foreach (CustomLevel customLevel in customLevelList)
        {
            Label name = new Label();
            name.AddFontOverride("font", dFont2);
            name.Text = customLevel.CustomLevelName + "      ";
            name.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(name);

            Button btn = new Button();
            btn.RectMinSize = new Vector2(80, 30);
            btn.Text = "Edit";
            string[] arr = new string[1] { btn.Name };
            Godot.Collections.Array ar = new Godot.Collections.Array();
            ar.Add(btn);
            btn.Name = customLevel.CustomLevelName.ToString();
            btn.Connect("pressed", this, "EditLevel", ar);
            gridContainer.AddChild(btn);

            Button btn2 = new Button();
            btn2.RectMinSize = new Vector2(80, 30);
            btn2.Text = "Delete";
            string[] arr2 = new string[1] { btn2.Name };
            Godot.Collections.Array ar2 = new Godot.Collections.Array();
            ar2.Add(btn2);
            btn2.Name = customLevel.CustomLevelName.ToString() + " ";
            btn2.Connect("pressed", this, "DeleteLevel", ar2);
            gridContainer.AddChild(btn2);
        }

    }
    private void EditLevel(Button btn)
    {
        CustomLevel cl = customLevelList.Find(item => item.CustomLevelName == btn.Name.Trim());
        Global.CustomLevelId = cl.CustomLevelId;
        GetTree().ChangeScene("res://Presentation/EditLevel/EditLevelInit.tscn");
    }
    private void DeleteLevel(Button btn)
    {
        CustomLevel cl = customLevelList.Find(item => item.CustomLevelName == btn.Name.Trim());
        customLevelBL.DeleteCustomLevel(cl.CustomLevelId);
        ClearGrid();
        DisplayLevels();
    }
    private void ClearGrid()
    {
        foreach (Node child in gridContainer.GetChildren())
            gridContainer.RemoveChild(child);
    }
}
