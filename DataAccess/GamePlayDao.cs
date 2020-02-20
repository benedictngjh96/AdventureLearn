using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class GamePlayDao : Node
{
	public List<CampaignQuestion> GetLevelQuestions(int worldId, int sectionId, int levelId)
	{
		string query = String.Format("SELECT * FROM CampaignQuestion cq INNER JOIN Question q ON cq.QuestionId  = q.QuestionId "
			+ " WHERE cq.WorldId ={0} AND cq.SectionId = {1} AND cq.LevelId ={2}", worldId, sectionId, levelId);
		List<CampaignQuestion> campaignQuestionList;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			campaignQuestionList = conn.Query<CampaignQuestion, Question, CampaignQuestion>(query, (cq, q) =>
			{
				cq.Question = q;
				return cq;
			},
			splitOn: "QuestionId").Distinct().ToList();
		}
		return campaignQuestionList;
	}
	public Level GetLevel(int worldId, int sectionId, int levelId)
	{
		BaseDao<Level> baseDao = new BaseDao<Level>();
		string query = String.Format("SELECT * FROM Level l WHERE l.WorldId ={0} AND l.SectionId = {1} AND l.LevelId ={2}"
			, worldId, sectionId, levelId);
		Level level = baseDao.RetrieveQuery(query);
		return level;
	}
	public Monster GetMonster(int worldId, int sectionId, int levelId)
	{
		BaseDao<Monster> baseDao = new BaseDao<Monster>();
		string query = String.Format("SELECT m.MonsterId, m.MonsterName FROM CampaignQuestion cq INNER JOIN Monster m" +
			" ON cq.MonsterId = m.MonsterId WHERE WorldId = {0} AND SectionId = {1} AND LevelId = {2}"
			, worldId, sectionId, levelId);
		Monster monster = baseDao.RetrieveQuery(query);
		return monster;

	}
	public Character GetCharacter(int studentId)
	{
		BaseDao<Character> baseDao = new BaseDao<Character>();
		string query = String.Format("SELECT c.CharId, c.CharName, c.CharSkill FROM Student s "
			+ " INNER JOIN Characters c ON s.CharId = c.CharId WHERE s.StudentID = {0}", studentId);
		Character character = baseDao.RetrieveQuery(query);
		return character;
	}
}
