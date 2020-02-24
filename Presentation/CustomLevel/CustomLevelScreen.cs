using Godot;
using System;
using System.Collections.Generic;

public class CustomLevelScreen : Node2D
{
    GamePlay gamePlay;
    CustomLevelBL customLevelBL;
    CustomLevel customLevel;

    public override void _Ready()
    {
        //REMOVE 
        Global.AssignmentId = 1;
        Global.WorldId = 1;
        Global.SectionId = 1;
        Global.LevelId = 1;
        Global.StudentId = 1;
        Global.CustomLevelId = 1;

        customLevelBL = new CustomLevelBL();
        customLevel = customLevelBL.GetCustomLevel(Global.CustomLevelId);
        //Child node instance
        var gamePlayScene = ResourceLoader.Load("res://Presentation/GamePlay/GamePlay.tscn") as PackedScene;
        gamePlay = gamePlayScene?.Instance() as GamePlay;
        AddChild(gamePlay);


        SetSpritesPath();
        gamePlay.SetQuestionList(customLevel.Question);
        gamePlay.SetTimeLimit(customLevel.TimeLimit);
        gamePlay.SetLevelTitle(customLevel.CustomLevelName);
        gamePlay.DisplayQuestion();
        gamePlay.SetQuestionNum();
       
    }

    private void SetSpritesPath()
    {
        CharacterBL characterBL = new CharacterBL();
        gamePlay.DisplayCharSprite(characterBL.GetCharacter(Global.StudentId));
        gamePlay.DisplayMonsterSprite(customLevel.Monster);
    }
}
