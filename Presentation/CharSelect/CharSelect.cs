using Godot;
using System;
using System.Collections.Generic;

public class CharSelect : Node2D
{
    StudentBL studentBL;
    int charId;
    public override void _Ready()
    {
        studentBL = new StudentBL();
        
        //Redirect user if has existing account
        if (studentBL.CheckStudentExist())
        {
            //GetTree().ChangeScene("res://Presentation/Login/Login.tscn");
            GD.Print("exist");
        }
    }

    private void _on_Knight1Button_pressed()
    {
        DisplayCharacter("Knight1");
    }
    private void _on_Knight2Button_pressed()
    {
        DisplayCharacter("Knight2");
    }
    private void DisplayCharacter(String characterName)
    {
        AnimatedSprite charSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        Button enterBtn = GetNode<Button>("EnterBtn");
        Label skillDescription = GetNode<Label>("SkillDescription");

        charSprite.Play(characterName, false);
        charSprite.SetVisible(true);
        enterBtn.SetVisible(true);

        //Find character obj in characterlist using charactername
        List<Character> characterList = studentBL.GetCharacterList();
        Character result = characterList.Find(item => item.CharName == characterName);
        skillDescription.SetText(result.CharSkill);
        charId = result.CharId;
        
    }
  
    private void _on_EnterBtn_pressed()
    {
        GD.Print(studentBL.CheckStudentExist().ToString());
        studentBL.InsertStudent(Global.studentId, Global.studentName, "testName", charId);
    }

}
