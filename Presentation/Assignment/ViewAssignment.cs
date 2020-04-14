using Godot;
using System;
using System.Collections.Generic;

public class ViewAssignment : Node2D
{

    VBoxContainer vbox;
    DynamicFont dFont;
    DynamicFont dFont2;
    GridContainer gridContainer;
    AssignmentBL assignmentBL;
    List<PublishedAssignment> assignmentList;
    AssignmentScoreBL assignmentScoreBL;
    TextureButton nextBtn;
    TextureButton prevBtn;
    Sprite title;
    public override void _Ready()
    {
        vbox = GetNode<VBoxContainer>("VBoxContainer");
        gridContainer = GetNode<GridContainer>("VBoxContainer/GridContainer");
        assignmentBL = new AssignmentBL();
        assignmentScoreBL = new AssignmentScoreBL();
        nextBtn = GetNode<TextureButton>("NextBtn");
        prevBtn = GetNode<TextureButton>("PrevBtn");
        title = GetNode<Sprite>("Title");
        gridContainer = GetNode<GridContainer>("VBoxContainer/GridContainer");
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 26;


        dFont2 = new DynamicFont();
        dFont2.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont2.Size = 18;
        assignmentList = assignmentBL.GetStudentAssignment(Global.StudentId);
        DisplayHeader();
        DisplayAssignment();
    }
    private void DisplayAssignment()
    {
        foreach (PublishedAssignment assignment in assignmentList)
        {
            Label name = new Label();
            name.AddFontOverride("font", dFont2);
            name.Text = assignment.Assignment.AssignmentName;
            name.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(name);

            Label teacher = new Label();
            teacher.AddFontOverride("font", dFont2);
            teacher.Text = assignment.Assignment.Teacher.TeacherName;
            teacher.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(teacher);

            Label date = new Label();
            date.AddFontOverride("font", dFont2);
            date.Text = assignment.DueDate.ToString();
            date.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(date);

            if (DateTime.Compare(DateTime.Now, assignment.DueDate) > 0)
            {
                Label expire = new Label();
                expire.AddFontOverride("font", dFont);
                expire.Text = "";
                expire.AddColorOverride("font_color", new Color(0, 0, 0));
                gridContainer.AddChild(expire);
            }
            else
            {
                Theme theme = ResourceLoader.Load("res://Assets/GUI/BtnUI4.tres") as Theme;
                Button btn = new Button();
                btn.RectMinSize = new Vector2(80, 30);
                btn.Text = "Play";
                btn.Theme = theme;
                string[] arr = new string[1] { btn.Name };
                Godot.Collections.Array ar = new Godot.Collections.Array();
                ar.Add(btn);
                btn.Name = assignment.Assignment.AssignmentId.ToString();
                btn.Connect("pressed", this, "PlayAssignment", ar);
                gridContainer.AddChild(btn);
            }

        }
    }
    private void DisplayCompletedAssignments()
    {
        List<AssignmentScore> completedAssignments = assignmentScoreBL.GetStudentCompletedAssignment(Global.StudentId);
        foreach (AssignmentScore as2 in completedAssignments)
        {
            Label name = new Label();
            name.AddFontOverride("font", dFont2);
            name.Text = as2.PublishedAssignment.Assignment.AssignmentName;
            name.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(name);

            Label teacher = new Label();
            teacher.AddFontOverride("font", dFont2);
            teacher.Text = as2.PublishedAssignment.Assignment.Teacher.TeacherName;
            teacher.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(teacher);

            Label date = new Label();
            date.AddFontOverride("font", dFont2);
            date.Text = as2.PublishedAssignment.DueDate.ToString()+"     ";
            date.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(date);

            Label score = new Label();
            score.AddFontOverride("font", dFont2);
            score.Text = as2.Score.ToString();
            score.AddColorOverride("font_color", new Color(0, 0, 0));
            gridContainer.AddChild(score);


        }
    }
    private void PlayAssignment(Button btn)
    {
        Global.AssignmentId = Convert.ToInt32(btn.Name);
        GetTree().ChangeScene("res://Presentation/Assignment/Assignment.tscn");
    }
    private void DisplayHeader()
    {
        //Student Name
        Label lbl = new Label();
        lbl.AddFontOverride("font", dFont);
        lbl.Text = "Assignment" + "       ";
        lbl.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl);

        Label lbl3 = new Label();
        lbl3.AddFontOverride("font", dFont);
        lbl3.Text = "Teacher" + "      ";
        lbl3.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl3);

        Label date = new Label();
        date.AddFontOverride("font", dFont);
        date.Text = "DueDate" + "      ";
        date.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(date);

        Label lbl2 = new Label();
        lbl2.AddFontOverride("font", dFont);
        lbl2.Text = " ";
        lbl2.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl2);
    }
    private void DisplayCompletedHeader()
    {
        //Student Name
        Label lbl = new Label();
        lbl.AddFontOverride("font", dFont);
        lbl.Text = "Assignment" + "       ";
        lbl.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl);

        Label lbl3 = new Label();
        lbl3.AddFontOverride("font", dFont);
        lbl3.Text = "Teacher" + "      ";
        lbl3.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl3);

        Label date = new Label();
        date.AddFontOverride("font", dFont);
        date.Text = "DueDate" + "      ";
        date.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(date);

        Label lbl2 = new Label();
        lbl2.AddFontOverride("font", dFont);
        lbl2.Text = "Score";
        lbl2.AddColorOverride("font_color", new Color(0, 0, 0));
        gridContainer.AddChild(lbl2);
    }
    private void ClearGrid()
    {
        foreach (Node child in gridContainer.GetChildren())
            gridContainer.RemoveChild(child);
    }
    private void _on_NextBtn_pressed()
    {
        Texture texture = ResourceLoader.Load("res://Assets/GUI/clearedtitle.png") as Texture;
        title.Texture = texture;

        ClearGrid();
        DisplayCompletedHeader();
        DisplayCompletedAssignments();
        nextBtn.Disabled = true;
        prevBtn.Disabled = false;
    }

    private void _on_PrevBtn_pressed()
    {
        Texture texture = ResourceLoader.Load("res://Assets/GUI/assignmenttitle.png") as Texture;
        title.Texture = texture;

        ClearGrid();
        DisplayHeader();
        DisplayAssignment();
        prevBtn.Disabled = true;
        nextBtn.Disabled = false;
    }

}


