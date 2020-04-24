using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for Character Select
/// </summary>
public class CharSelect : Node2D
{

    StudentBL studentBL;
    List<Character> characterList;
    TextureButton enterBtn;
    CharacterBL characterBL;
    int charId;
    string charSelected = "";
    AnimatedSprite charSprite;
    Sprite skillIcon;
    Label charName;
    Label nameLbl;
    Label skillNameLbl;
    Label enterLbl;
    Label skillDescriptionLbl;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        //Hide animated sprite and button when user has not selected a character
        charSprite = GetNode<AnimatedSprite>("Char/AnimatedSprite");
        enterBtn = GetNode<TextureButton>("EnterBtn");
        charSprite.Visible = false;
        enterBtn.Visible = false;
        studentBL = new StudentBL();
        characterBL = new CharacterBL();
        charName = GetNode<Label>("Char/CharName");
        nameLbl = GetNode<Label>("Char/Name");
        skillNameLbl = GetNode<Label>("Char/SkillName");
        skillDescriptionLbl = GetNode<Label>("Char/SkillDescription");
        skillIcon = GetNode<Sprite>("Char/SkillIcon");
        enterLbl = GetNode<Label>("EnterBtnLbl");
        characterList = characterBL.GetAllCharacters();

    }

    /// <summary>
    /// Handles the logic when this character is pressed
    /// </summary>
    private void _on_Knight1_pressed()
    {
        DisplayCharacter("Escanor");
        charSelected = "Escanor";
    }

    /// <summary>
    /// Handles the logic when this character is pressed
    /// </summary>
    private void _on_Warrior1_pressed()
    {
        DisplayCharacter("Athena");
        charSelected = "Athena";
    }

    /// <summary>
    /// Handles the logic when this character is pressed
    /// </summary>
    private void _on_Warrior2_pressed()
    {
        DisplayCharacter("Mjolnir");
        charSelected = "Mjolnir";
    }

    /// <summary>
    /// Handles the logic when this character is pressed
    /// </summary>
    private void _on_Zeus_pressed()
    {
        DisplayCharacter("Zeus");
        charSelected = "Zeus";
    }

    /// <summary>
    /// Display the Character stated in the parameter
    /// </summary>
    /// <param name="characterName"></param>
    private void DisplayCharacter(String characterName)
    {
        charSprite.Play(characterName + "Walk", false);
        charSprite.Visible = true;
        enterBtn.Visible = true;
        nameLbl.Visible = true;
        skillNameLbl.Visible = true;
        enterLbl.Visible = true;
        charName.Text = characterName;
        if (characterName == "Athena")
            charSprite.Position = new Vector2(510.527f, 323.009f);
        else
            charSprite.Position = new Vector2(510.527f, 256.387f);
        //Find character obj in characterlist using charactername
        Character result = characterList.Find(item => item.CharName == characterName);
        skillNameLbl.Text = result.CharSkill;
        skillDescriptionLbl.Text = result.SkillDescription;

        Texture texture = ResourceLoader.Load(String.Format("res://Skills/Icons/{0}.png", result.CharSkill)) as Texture;
        skillIcon.Texture = texture;
        charId = result.CharId;
    }

    /// <summary>
    /// Handles the logic when the Enter button is pressed
    /// </summary>
    private void _on_EnterBtn_pressed()
    {
        StudentBL studentBL = new StudentBL();
        studentBL.UpdateStudentCharacter(charId, Global.StudentId);
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
    }

    /// <summary>
    /// Handles the logic when the Back button is pressed
    /// </summary>
    private void _on_BackBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/UserProfile/UserProfile.tscn");
    }
}


