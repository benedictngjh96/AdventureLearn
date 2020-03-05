using Godot;
using System;
using System.Collections.Generic;
public class UserProfile : Node2D
{
    StudentBL studentBL;
    public override void _Ready()
    {
        studentBL = new StudentBL();
        Global.StudentId = 1;
        Student student = studentBL.GetStudentCharacter(Global.StudentId);
        Label lbl = GetNode<Label>("NameLbl");
        lbl.Text = student.StudentName;
        AnimatedSprite character = GetNode<AnimatedSprite>("Character");
        Label charName = GetNode<Label>("CharacterName");
        Label charSkill = GetNode<Label>("CharacterSkill");
        charName.Text = student.Character.CharName;
        charSkill.Text = student.Character.CharSkill;
        string spritePath = "res://CharSprites/";
        string charPath = String.Format(spritePath + "{0}/", student.Character.CharName);
        List<string> animationList = new List<string>();
        animationList.Add("Idle");
        Global.LoadSprite(charPath, character, animationList);
        StudentScoreBL studentScoreBL = new StudentScoreBL();
        Label avgScoreLbl = GetNode<Label>("AvgScoreLbl");
        avgScoreLbl.Text = studentScoreBL.GetAvgWorldScores(Global.StudentId).LevelScore.ToString();

    }
    private void _on_ChangeCharacter_pressed()
    {
        GetTree().ChangeScene("res://Presentation/CharSelect/CharSelect.tscn");
    }

}



