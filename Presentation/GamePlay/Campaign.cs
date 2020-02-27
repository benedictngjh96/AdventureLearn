using Godot;
using System;
using System.Collections.Generic;

public class Campaign : Node2D
{
    GamePlay gamePlay;
    CampaignBL campaignBL;
    List<Question> questionList;
    int timeLimit = 0;
    Level level;
    public override void _Ready()
    {
        campaignBL = new CampaignBL();
        level = campaignBL.GetLevel(Global.WorldId, Global.SectionId, Global.LevelId);

        //Child node instance
        var gamePlayScene = ResourceLoader.Load("res://Presentation/GamePlay/GamePlay.tscn") as PackedScene;
        gamePlay = gamePlayScene?.Instance() as GamePlay;
        AddChild(gamePlay);
        gamePlay.Connect("NoMoreQuestions", this, nameof(InsertStudentScore));

        SetSpritesPath();
        gamePlay.SetQuestionList(level.Question);
        gamePlay.SetTimeLimit(level.TimeLimit);
        gamePlay.DisplayQuestion();
        gamePlay.SetQuestionNum();
    }
    private void SetSpritesPath()
    {
        CharacterBL characterBL = new CharacterBL();
        gamePlay.DisplayCharSprite(characterBL.GetCharacter(Global.StudentId));
        gamePlay.DisplayMonsterSprite(level.Monster);
    }

    private void InsertStudentScore()
    {
        StudentScoreBL studentScoreBL = new StudentScoreBL();
        int result = studentScoreBL.InsertStudentScore(Global.StudentId, Global.WorldId, Global.SectionId, Global.LevelId, gamePlay.GetTimeLeft(), level.TimeLimit);
        GD.Print(result);
    }

}
