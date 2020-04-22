using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for AssignmentScreen (gameplay)
/// </summary>
public class AssignmentScreen : Node2D
{
    GamePlay gamePlay;
    AssignmentBL assignmentBL;
    Assignment assignment;
    CharacterBL characterBL;
    Character character;

    Monster monster;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        assignmentBL = new AssignmentBL();
        characterBL = new CharacterBL();
        monster = assignmentBL.GetAssignmentMonster(Global.AssignmentId);
        //Child node instance
        var gamePlayScene = ResourceLoader.Load("res://Presentation/GamePlay/GamePlay.tscn") as PackedScene;
        gamePlay = gamePlayScene?.Instance() as GamePlay;
        AddChild(gamePlay);
        gamePlay.Connect("NoMoreQuestions", this, nameof(InsertAssignmentScore));
        assignment = assignmentBL.GetAssignment(Global.AssignmentId);
        character = characterBL.GetCharacter(Global.StudentId);

        SetSpritesPath();
        gamePlay.SetCharacter(character);
        gamePlay.SetQuestionList(assignment.Question);
        gamePlay.SetTimeLimit(assignment.TimeLimit);
        gamePlay.SetLevelTitle(assignment.AssignmentName);
        gamePlay.DisplayQuestion();
        gamePlay.SetQuestionNum();
        gamePlay.SetGameType("Assignment");
        gamePlay.LoadStart(character, monster);
    }

    /// <summary>
    /// Call the methods in Gameplay to setup sprites path for the Student Character the Assignment Monster and display them
    /// </summary>
    private void SetSpritesPath()
    {
        gamePlay.DisplayCharSprite(character);
        gamePlay.DisplayMonsterSprite(assignment.Monster);
    }
    
    /// <summary>
    /// Insert Assignment score through business logic
    /// </summary>
    private void InsertAssignmentScore()
    {
        AssignmentScoreBL assignmentScoreBL = new AssignmentScoreBL();
        assignmentScoreBL.InsertAssignmentScore(Global.StudentId, Global.AssignmentId, gamePlay.GetTimeLeft(), assignment.TimeLimit);
    }

}
