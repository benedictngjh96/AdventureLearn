using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for CustomLevelScreen (gameplay)
/// </summary>
public class CustomLevelScreen : Node2D
{
    GamePlay gamePlay;
    CustomLevelBL customLevelBL;
    CustomLevel customLevel;
    CharacterBL characterBL;
    Character character;
    Monster monster;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        customLevelBL = new CustomLevelBL();
        characterBL = new CharacterBL();
        customLevel = customLevelBL.GetCustomLevel(Global.CustomLevelId);
        character = characterBL.GetCharacter(Global.StudentId);
        monster = customLevelBL.GetCustomLevelMonster(Global.CustomLevelId);
        //Child node instance
        var gamePlayScene = ResourceLoader.Load("res://Presentation/GamePlay/GamePlay.tscn") as PackedScene;
        gamePlay = gamePlayScene?.Instance() as GamePlay;
        AddChild(gamePlay);
        gamePlay.Connect("NoMoreQuestions", this, nameof(InsertCustomLevelScore));

        SetSpritesPath();
        gamePlay.SetCharacter(character);
        gamePlay.SetQuestionList(customLevel.Question);
        gamePlay.SetTimeLimit(customLevel.TimeLimit);
        gamePlay.SetLevelTitle(customLevel.CustomLevelName);
        gamePlay.DisplayQuestion();
        gamePlay.SetQuestionNum();
        gamePlay.SetGameType("CustomLevel");
        gamePlay.LoadStart(character, monster);
       
    }

    /// <summary>
    /// Call the methods in Gameplay to setup sprites path for the Student Character the Assignment Monster and display them
    /// </summary>
    private void SetSpritesPath()
    {
        CharacterBL characterBL = new CharacterBL();
        gamePlay.DisplayCharSprite(characterBL.GetCharacter(Global.StudentId));
        gamePlay.DisplayMonsterSprite(customLevel.Monster);
    }

    /// <summary>
    /// Insert CustomLevel score through business logic
    /// </summary>
    private void InsertCustomLevelScore()
    {
        CustomLevelScoreBL customLevelScoreBL = new CustomLevelScoreBL();
        customLevelScoreBL.InsertCustomLevelScore(Global.StudentId, Global.CustomLevelId, gamePlay.GetTimeLeft(), customLevel.TimeLimit);
    }
}
