using Godot;
using System;
using System.Collections.Generic;

public class GamePlayBL : Node
{
	GamePlayDao gamePlayDao = new GamePlayDao();
	public List<CampaignQuestion> GetLevelQuestions()
	{
		List<CampaignQuestion> campaignQuestionList = gamePlayDao.GetLevelQuestions(Global.WorldId, Global.SectionId, Global.LevelId);

		return campaignQuestionList;
	}
	public int GetTimeLimit()
	{
		Level lvl = gamePlayDao.GetLevel(Global.WorldId, Global.SectionId, Global.LevelId);

		return lvl.TimeLimit;
	}
	public string GetMonsterName()
	{
		GamePlayDao gamePlayDao = new GamePlayDao();

		Monster monster = gamePlayDao.GetMonster(Global.WorldId,Global.SectionId,Global.LevelId);
		GD.Print(monster.MonsterName);

		return monster.MonsterName;
	}
	public Character GetCharacter()
	{
		GamePlayDao gamePlayDao = new GamePlayDao();
		Character character = gamePlayDao.GetCharacter(Global.StudentId);
		return character;
	}
	

}
