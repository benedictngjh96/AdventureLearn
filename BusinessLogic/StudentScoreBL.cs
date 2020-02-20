using Godot;
using System;
using System.Collections.Generic;

public class StudentScoreBL : Node
{
	StudentScoreDao studentScoreDao = new StudentScoreDao();

	public Student GetStudentScores()
	{
		Student student = studentScoreDao.GetStudentScores(Global.SectionId, Global.StudentId);
		return student;
	}
	public List<Student> GetAllStudentScores()
	{
		List<Student> studentList = studentScoreDao.GetAllStudentScores(Global.SectionId);
		return studentList;

	}
	public void InsertStudentScore(int timeRemaining, int timeLimit)
	{
		StudentScoreDao studentScoreDao = new StudentScoreDao();
		double levelScore = Convert.ToDouble(timeRemaining) / Convert.ToDouble(timeLimit) * 100;
		int score = Convert.ToInt32(levelScore);
		/*
		switch (levelScore)
		{
			case int n when (n >= 1 && n <= 50):
				levelScore = 1;
				break;
			case int n when (n >= 51 && n <= 70):
				levelScore = 2;
				break;
			case int n when (n >= 71 && n <= 100):
				levelScore = 3;
				break;
		}
		*/
		if(CheckScoreExist() == 0)
			studentScoreDao.InsertStudentScore(Global.StudentId, Global.WorldId, Global.SectionId, Global.LevelId, score);
	}
	public int CheckScoreExist()
	{
		StudentScoreDao studentScoreDao = new StudentScoreDao();
		int result = studentScoreDao.CheckScoreExist(Global.StudentId, Global.LevelId);
		return result;
	}
}
