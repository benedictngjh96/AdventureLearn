using Godot;
using System;
using System.Collections.Generic;

public class AssignmentScreen : Node2D
{
    GamePlay gamePlay;
    AssignmentBL assignmentBL;
	Assignment assignment;

    public override void _Ready()
    {
		//REMOVE
		Global.AssignmentId = 1;
		Global.WorldId = 1;
		Global.SectionId = 1;
		Global.LevelId = 1;
		Global.StudentId = 1;
		

		assignmentBL = new AssignmentBL();

		//Child node instance
		var gamePlayScene = ResourceLoader.Load("res://Presentation/GamePlay/GamePlay.tscn") as PackedScene;
		gamePlay = gamePlayScene?.Instance() as GamePlay;
		AddChild(gamePlay);
		gamePlay.Connect("NoMoreQuestions", this, nameof(InsertAssignmentScore));
		assignment = assignmentBL.GetAssignment(Global.AssignmentId);

		SetSpritesPath();
		gamePlay.SetQuestionList(assignment.Question);
		gamePlay.SetTimeLimit(assignment.TimeLimit);
		gamePlay.SetLevelTitle(assignment.AssignmentName);
		gamePlay.DisplayQuestion();
		gamePlay.SetQuestionNum();
	}
	private void SetSpritesPath()
	{
		CharacterBL characterBL = new CharacterBL();
		gamePlay.DisplayCharSprite(characterBL.GetCharacter(Global.StudentId));
		gamePlay.DisplayMonsterSprite(assignment.Monster);
	}
	private void InsertAssignmentScore()
	{
		AssignmentScoreBL assignmentScoreBL = new AssignmentScoreBL();
		assignmentScoreBL.InsertAssignmentScore(Global.StudentId, Global.AssignmentId, gamePlay.GetTimeLeft(), assignment.TimeLimit);
	}

}
