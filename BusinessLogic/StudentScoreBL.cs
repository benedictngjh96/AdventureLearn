using System;
using System.Collections.Generic;

public class StudentScoreBL 
{
	StudentScoreDao studentScoreDao = new StudentScoreDao();

	public Student GetStudentScores(int sectionId, int studentId)
	{
		Student student = studentScoreDao.GetStudentScores(sectionId, studentId);
		return student;
	}
	public List<Student> GetAllStudentScores(int sectionId)
	{
		List<Student> studentList = studentScoreDao.GetAllStudentScores(sectionId);
		return studentList;

	}
	public void InsertStudentScore(int studentId, int worldId, int sectionId, int levelId, int timeRemaining, int timeLimit)
	{
		StudentScoreDao studentScoreDao = new StudentScoreDao();
		double levelScore = Convert.ToDouble(timeRemaining) / Convert.ToDouble(timeLimit) * 100;
		int score = Convert.ToInt32(levelScore);
		if(CheckScoreExist(studentId, levelId) == 0)
			studentScoreDao.InsertStudentScore(studentId, worldId, sectionId, levelId, score);
	}
	public int CheckScoreExist(int studentId, int levelId)
	{
		StudentScoreDao studentScoreDao = new StudentScoreDao();
		int result = studentScoreDao.CheckScoreExist(studentId, levelId);
		return result;
	}
}
