using Godot;
using System;
using System.Collections.Generic;

public class CharSelect : Node2D
{
    
    StudentBL studentBL;
    List<Character> characterList;
    AnimatedSprite charSprite;
    Button enterBtn;
    CharacterBL characterBL;
    int charId;
    public override void _Ready()
    {
        //Hide animated sprite and button when user has not selected a character
        charSprite = GetNode<AnimatedSprite>("Char/AnimatedSprite");
        enterBtn = GetNode<Button>("EnterBtn");
        charSprite.Visible = false;
        enterBtn.Visible = false;
        studentBL = new StudentBL();
        characterBL = new CharacterBL();

        characterList = characterBL.GetAllCharacters();
        Godot.GD.Print(Global.StudentId);
       
  
    }

    private void _on_Knight1_pressed()
    {
        DisplayCharacter("Knight1");
    }
    private void _on_Knight2_pressed()
    {
        DisplayCharacter("Knight2");
    }
    private void DisplayCharacter(String characterName)
    {
        Label skillDescription = GetNode<Label>("Char/SkillDescription");
        charSprite.Play(characterName, false);
        charSprite.Visible = true;
        enterBtn.Visible = true;

        //Find character obj in characterlist using charactername
        Character result = characterList.Find(item => item.CharName == characterName);
        skillDescription.Text = result.CharSkill;
        charId = result.CharId;
    }
  
    private void _on_EnterBtn_pressed()
    {
        StudentBL studentBL = new StudentBL();
        studentBL.UpdateStudentCharacter(charId, Global.StudentId);
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
    }

}
