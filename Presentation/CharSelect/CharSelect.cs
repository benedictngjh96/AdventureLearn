using Godot;
using System;
using System.Collections.Generic;

public class CharSelect : Node2D
{

    StudentBL studentBL;
    List<Character> characterList;
    Button enterBtn;
    CharacterBL characterBL;
    int charId;
    string charSelected = "";
    AnimatedSprite charSprite;
	Label charName;
    public override void _Ready()
    {
        //Hide animated sprite and button when user has not selected a character
        charSprite = GetNode<AnimatedSprite>("Char/AnimatedSprite");
        enterBtn = GetNode<Button>("EnterBtn");
        charSprite.Visible = false;
        enterBtn.Visible = false;
        studentBL = new StudentBL();
        characterBL = new CharacterBL();
		charName= GetNode<Label>("Char/CharName");
        characterList = characterBL.GetAllCharacters();
        Godot.GD.Print(Global.StudentId);


    }
    private void _on_Knight1_pressed()
    {
        DisplayCharacter("Escanor");
        charSelected = "Escanor";
    }
    private void _on_Warrior1_pressed()
    {
        DisplayCharacter("Athena");
        charSelected = "Athena";
    }
    private void _on_Warrior2_pressed()
    {
        DisplayCharacter("Mjolnir");
        charSelected = "Mjolnir";
    }
    private void _on_Zeus_pressed()
    {
        DisplayCharacter("Zeus");
        charSelected = "Zeus";
    }
    private void DisplayCharacter(String characterName)
    {
        Label skillDescription = GetNode<Label>("Char/SkillDescription");
        charSprite.Play(characterName + "Walk", false);
        charSprite.Visible = true;
        enterBtn.Visible = true;
		charName.Text = characterName;
        if (characterName == "Athena")
            charSprite.Position = new Vector2(478.243f, 323.009f);
        else
            charSprite.Position = new Vector2(478.243f, 256.387f);
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
    private void _on_AnimatedSprite_animation_finished()
    {
        charSprite.Play(charSelected + "Attack");
    }

}
