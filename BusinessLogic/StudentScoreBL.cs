using System;
using System.Collections.Generic;

public class StudentScoreBL
{
	StudentScoreDao studentScoreDao = new StudentScoreDao();

	/// <summary>
	/// Return Student object according to sectionId, studentId
	/// </summary>
	/// <param name="sectionId"></param>
	/// <param name="studentId"></param>
	/// <returns></returns>
	public Student GetStudentScores(int worldId, int sectionId, int studentId)
	{
		Student student = studentScoreDao.GetStudentScores(worldId, sectionId, studentId);
		return student;
	}
	/// <summary>
	/// Return list of Student according to sectionId
	/// </summary>
	/// <param name="sectionId"></param>
	/// <returns></returns>
	public List<Student> GetAllStudentScores(int sectionId)
	{
		List<Student> studentList = studentScoreDao.GetAllStudentScores(sectionId);
		return studentList;

	}
	/// <summary>
	/// Return int result 1 if InsertStudentScore has executed successful
	/// </summary>
	/// <param name="studentId"></param>
	/// <param name="worldId"></param>
	/// <param name="sectionId"></param>
	/// <param name="levelId"></param>
	/// <param name="timeRemaining"></param>
	/// <param name="timeLimit"></param>
	/// <returns></returns>
	public int InsertStudentScore(int studentId, int worldId, int sectionId, int levelId, int timeRemaining, int timeLimit)
	{
		StudentScoreDao studentScoreDao = new StudentScoreDao();
		return studentScoreDao.InsertStudentScore(studentId, worldId, sectionId, levelId, Global.CalculateScore(timeRemaining, timeLimit));
	}
}
