using Godot;
using System;
using System.Collections.Generic;

public class ViewCustomLevel : Node2D
{
    CustomLevelBL customLevelBL;
    VBoxContainer vbox;
    DynamicFont dFont;
    DynamicFont dFont2;
    GridContainer gridContainer;
    TextureButton nextBtn;
    TextureButton prevBtn;
    List<CustomLevel> customLevelList;
    Label title;
    public override void _Ready()
    {
        customLevelBL = new CustomLevelBL();
        customLevelList = customLevelBL.GetCustomLevels();
        vbox = GetNode<VBoxContainer>("VBoxContainer");
        nextBtn = GetNode<TextureButton>("NextBtn");
        prevBtn = GetNode<TextureButton>("PrevBtn");
        title = GetNode<Label>("Title");
        gridContainer = GetNode<GridContainer>("VBoxContainer/GridContainer");
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 26;

        dFont2 = new DynamicFont();
        dFont2.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont2.Size = 15;

        prevBtn.Disabled = true;
        DisplayHeader();
        DisplayGameList();
    }
    private void DisplayHeader()
    {
        //Student Name
        Label lbl = new Label();
        lbl.AddFontOverride("font", dFont);
        lbl.Text = "Game Name" + "       ";
        gridContainer.AddChild(lbl);

        Label lbl3 = new Label();

        lbl3.AddFontOverride("font", dFont);
        lbl3.Text = "Game Creator" + "      ";
        gridContainer.AddChild(lbl3);

        Label lbl2 = new Label();
        lbl2.AddFontOverride("font", dFont);
        lbl2.Text = " ";
        gridContainer.AddChild(lbl2);
    }
    private void DisplayHistoryHeader()
    {
        //Student Name
        Label lbl = new Label();
        lbl.AddFontOverride("font", dFont);
        lbl.Text = "Game Name" + "       ";
        gridContainer.AddChild(lbl);

        Label lbl3 = new Label();

        lbl3.AddFontOverride("font", dFont);
        lbl3.Text = "Game Creator" + "      ";
        gridContainer.AddChild(lbl3);

        Label lbl2 = new Label();
        lbl2.AddFontOverride("font", dFont);
        lbl2.Text = "Score";
        gridContainer.AddChild(lbl2);

        Label lbl4 = new Label();
        lbl4.AddFontOverride("font", dFont);
        lbl4.Text = " ";
        gridContainer.AddChild(lbl4);
    }
    private void ClearGrid()
    {
        foreach (Node child in gridContainer.GetChildren())
            gridContainer.RemoveChild(child);
    }
    private void DisplayGameList()
    {
        ClearGrid();
        DisplayHeader();
        gridContainer.Columns = 3;
        foreach (CustomLevel cl in customLevelList)
        {
            Label lbl3 = new Label();
            lbl3.AddFontOverride("font", dFont2);
            lbl3.Text = cl.CustomLevelName + "      ";
            gridContainer.AddChild(lbl3);

            Label lbl2 = new Label();
            lbl2.AddFontOverride("font", dFont2);
            lbl2.Text = cl.Student.StudentName;
            gridContainer.AddChild(lbl2);

            Button btn = new Button();
            btn.RectMinSize = new Vector2(80, 30);
            btn.Text = "Play";
            string[] arr = new string[1] { btn.Name };
            Godot.Collections.Array ar = new Godot.Collections.Array();
            ar.Add(btn);
            btn.Name = cl.CustomLevelId.ToString();
            btn.Connect("pressed", this, "PlayLevel", ar);
            gridContainer.AddChild(btn);
        }
    }
    private void PlayLevel(Button btn)
    {
        Global.CustomLevelId = Convert.ToInt32(btn.Name);
        GetTree().ChangeScene("res://Presentation/CustomLevel/CustomLevel.tscn");
    }
    private void DisplayClearedCustomLevels()
    {
        ClearGrid();
        DisplayHistoryHeader();
        gridContainer.Columns = 4;
        List<CustomLevelScore> customLevelScore = customLevelBL.GetClearedCustomLevels(Global.StudentId);
        GD.Print(customLevelScore.Count);
        foreach (CustomLevelScore cls in customLevelScore)
        {
            Label lbl3 = new Label();
            lbl3.AddFontOverride("font", dFont2);
            lbl3.Text = cls.CustomLevel.CustomLevelName + "      ";
            gridContainer.AddChild(lbl3);

            Label lbl5 = new Label();
            lbl5.AddFontOverride("font", dFont2);
            lbl5.Text = cls.Student.StudentName + "      ";
            gridContainer.AddChild(lbl5);

            Label lbl4 = new Label();
            lbl4.AddFontOverride("font", dFont2);
            lbl4.Text = cls.LevelScore + "      ";
            gridContainer.AddChild(lbl4);

            Button btn = new Button();
            btn.RectMinSize = new Vector2(80, 30);
            btn.Text = "Play";
            string[] arr = new string[1] { btn.Name };
            Godot.Collections.Array ar = new Godot.Collections.Array();
            ar.Add(btn);
            //Button da = (Button)GetNode("btn");
            btn.Name = cls.CustomLevel.CustomLevelId.ToString();
            btn.Connect("pressed", this, "JoinLevel", ar);

            gridContainer.AddChild(btn);

        }
    }
    private void _on_NextBtn_pressed()
    {
        title.Text = "Cleared Games";
        DisplayHistoryHeader();
        DisplayClearedCustomLevels();
        nextBtn.Disabled = true;
        prevBtn.Disabled = false;
    }


    private void _on_PrevBtn_pressed()
    {
        title.Text = "Custom Games";
        DisplayHeader();
        DisplayGameList();
        prevBtn.Disabled = true;
        nextBtn.Disabled = false;
    }
    private void _on_ViewCreatedBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/CustomLevel/ViewCustomLevel.tscn");
    }
}










