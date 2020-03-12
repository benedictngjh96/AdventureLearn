using Godot;
using System;
using System.Collections.Generic;

public class CustomLevelScreen : Node2D
{
<<<<<<< Updated upstream
    GamePlay gamePlay;
    CustomLevelBL customLevelBL;
    CustomLevel customLevel;
    CharacterBL characterBL;
    Character character;
    public override void _Ready()
    {
        //REMOVE 
        Global.StudentId = 1;
        Godot.GD.Print(Global.CustomLevelId);
        customLevelBL = new CustomLevelBL();
        characterBL = new CharacterBL();
        customLevel = customLevelBL.GetCustomLevel(Global.CustomLevelId);
        character = characterBL.GetCharacter(Global.StudentId);
=======
	GamePlay gamePlay;
	CustomLevelBL customLevelBL;
	CustomLevel customLevel;
	CharacterBL characterBL;
	Character character;
	public override void _Ready()
	{
		customLevelBL = new CustomLevelBL();
		characterBL = new CharacterBL();
		customLevel = customLevelBL.GetCustomLevel(Global.CustomLevelId);
		character = characterBL.GetCharacter(Global.StudentId);
>>>>>>> Stashed changes

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
	   
	}
	private void SetSpritesPath()
	{
		CharacterBL characterBL = new CharacterBL();
		gamePlay.DisplayCharSprite(characterBL.GetCharacter(Global.StudentId));
		gamePlay.DisplayMonsterSprite(customLevel.Monster);
	}
	private void InsertCustomLevelScore()
	{
		CustomLevelScoreBL customLevelScoreBL = new CustomLevelScoreBL();
		customLevelScoreBL.InsertCustomLevelScore(Global.StudentId, Global.CustomLevelId, gamePlay.GetTimeLeft(), customLevel.TimeLimit);
	}
}
