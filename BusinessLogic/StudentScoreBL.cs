using Godot;
using System;
using System.Collections.Generic;

public class StudentScoreBL : Node
{
	StudentScoreDao studentScoreDao = new StudentScoreDao();

	public Student GetStudentScores()
	{
		Student student = studentScoreDao.GetStudentScores(Global.sectionId, Global.studentId);
		return student;
	}
	public List<Student> GetAllStudentScores()
	{
		List<Student> studentList = studentScoreDao.GetAllStudentScores(Global.sectionId);
		return studentList;

	}
}
