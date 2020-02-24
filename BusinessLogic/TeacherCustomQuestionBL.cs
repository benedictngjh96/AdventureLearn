using System;
using System.Collections.Generic;

public class TeacherCustomQuestionBL 
{
	public List<TeacherCustomQuestion> GetCustomLevelQuestions()
	{
		TeacherCustomQuestionDao teacherDao = new TeacherCustomQuestionDao();
		return teacherDao.GetAssignmentQuestions(Global.AssignmentId);
	}
	public List<Question> GetQuestionList()
	{
		List<Question> questionList = new List<Question>();
		List<TeacherCustomQuestion> customQuestionList = GetCustomLevelQuestions();

		foreach (TeacherCustomQuestion customQuestion in customQuestionList)
		{
			questionList.Add(customQuestion.Question);
		}
		return questionList;
	}
	public int GetTimeLimit()
	{
		TeacherCustomQuestionDao teacherDao = new TeacherCustomQuestionDao();
		Assignment assignment = teacherDao.GetAssignment(Global.AssignmentId);
		return assignment.TimeLimit;
	}
	public Monster GetMonster()
	{
		TeacherCustomQuestionDao teacherDao = new TeacherCustomQuestionDao();
		return teacherDao.GetMonster(Global.AssignmentId);
	}

}
