using Godot;
using System;
using System.Collections.Generic;

public class Campaign : Node2D
{
    GamePlay gamePlay;
    CharacterBL characterBL;
    CampaignBL campaignBL;
    List<Question> questionList;
    int timeLimit = 0;
    Level level;
    Character character;
    public override void _Ready()
    {
        campaignBL = new CampaignBL();
        characterBL = new CharacterBL();
        level = campaignBL.GetLevel(Global.WorldId, Global.SectionId, Global.LevelId);
        character = characterBL.GetCharacter(Global.StudentId);
        //Child node instance
        var gamePlayScene = ResourceLoader.Load("res://Presentation/GamePlay/GamePlay.tscn") as PackedScene;
        gamePlay = gamePlayScene?.Instance() as GamePlay;
        AddChild(gamePlay);
        gamePlay.Connect("NoMoreQuestions", this, nameof(InsertStudentScore));
        SetSpritesPath();
        gamePlay.SetCharacter(character);
        gamePlay.SetQuestionList(level.Question);
        gamePlay.SetTimeLimit(level.TimeLimit);
        gamePlay.DisplayQuestion();
        gamePlay.SetQuestionNum();
        gamePlay.SetBg();
        gamePlay.LoadStart(character, level.Monster);
        gamePlay.SetGameType("Campaign");

    }
    private void SetSpritesPath()
    {

        gamePlay.DisplayCharSprite(character);
        gamePlay.DisplayMonsterSprite(level.Monster);
    }
    private void InsertStudentScore()
    {
        StudentScoreBL studentScoreBL = new StudentScoreBL();
        int result = studentScoreBL.InsertStudentScore(Global.StudentId, Global.WorldId, Global.SectionId, Global.LevelId, gamePlay.GetTimeLeft(), level.TimeLimit);
        int score = Global.CalculateScore(gamePlay.GetTimeLeft(), level.TimeLimit);
        int star = 0;
        switch (score)
        {
            case int ls when (ls >= 1 && ls <= 50):
                star = 1;
                break;
            case int ls when (ls >= 51 && ls <= 70):
                star = 2;
                break;
            case int ls when (ls >= 71 && ls <= 100):
                star = 3;
                break;
        }
        gamePlay.DisplayStars(star);
        //GetTree().ChangeScene("res://Presentation/World/World.tscn");
    }

}
