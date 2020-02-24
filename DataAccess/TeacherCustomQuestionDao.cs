using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class TeacherCustomQuestionDao 
{
	public List<TeacherCustomQuestion> GetAssignmentQuestions(int assignmentId)
	{
		string query = String.Format("SELECT * FROM TeacherCustomQuestion tcq INNER JOIN Question q ON tcq.QuestionId  = q.QuestionId  "
			+ " WHERE tcq.AssignmentId = {0}", assignmentId);
		List<TeacherCustomQuestion> customQuestionList;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			customQuestionList = conn.Query<TeacherCustomQuestion, Question, TeacherCustomQuestion>(query, (tcq, q) =>
			{
				tcq.Question = q;
				return tcq;
			},
			splitOn: "QuestionId").Distinct().ToList();
		}
		return customQuestionList;
	}
	public Assignment GetAssignment(int assignmentId)
	{
		BaseDao<Assignment> baseDao = new BaseDao<Assignment>();
		string query = String.Format("SELECT * FROM Assignment a WHERE a.AssignmentId ={0}"
			, assignmentId);
		Assignment assignment = baseDao.RetrieveQuery(query);
		return assignment;
	}

	public Monster GetMonster(int assignmentId)
	{
		BaseDao<Monster> baseDao = new BaseDao<Monster>();
		string query = String.Format("SELECT m.MonsterId, m.MonsterName FROM Assignment a INNER JOIN Monster m" +
			" ON a.MonsterId = m.MonsterId WHERE AssignmentId = {0}"
			, assignmentId);
		Monster monster = baseDao.RetrieveQuery(query);
		return monster;
	}
}
