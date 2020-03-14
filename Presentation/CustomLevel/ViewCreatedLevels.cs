using Godot;
using System;
using System.Collections.Generic;
public class ViewCreatedLevels : Node2D
{
    VBoxContainer vbox;
    GridContainer gridContainer;
    DynamicFont dFont;
    List<CustomLevel> customLevelList;
    CustomLevelBL customLevelBL;
    public override void _Ready()
    {
        vbox = GetNode<VBoxContainer>("VBoxContainer");
        gridContainer = GetNode<GridContainer>("VBoxContainer/GridContainer");

        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 15;
        customLevelBL = new CustomLevelBL();

        DisplayLevels();

    }
    private void DisplayLevels()
    {
        customLevelList = customLevelBL.GetStudentCustomLevel(Global.StudentId);
        foreach (CustomLevel customLevel in customLevelList)
        {
            Label name = new Label();
            name.AddFontOverride("font", dFont);
            name.Text = customLevel.CustomLevelName + "      ";
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
        GD.Print(btn.Name);
        CustomLevel cl = customLevelList.Find(item => item.CustomLevelName == btn.Name.Trim());
        Global.CustomLevelId = cl.CustomLevelId;
        GetTree().ChangeScene("res://Presentation/EditLevel/EditLevelInit.tscn");
        //Redirect to edit page
    }
    private void DeleteLevel(Button btn)
    {
        //AssignmentBL assignmentBL = new AssignmentBL();
        //Assignment a = assignmentList.Find(item => item.AssignmentName == btn.Name.Trim());
        //assignmentBL.DeleteAssignment(a.AssignmentId);
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
