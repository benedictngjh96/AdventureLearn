using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Godot;
public class StudentDao
{
	
	public int InsertStudent(string studentId, string studentName, string inGameName, int charId)
	{
		BaseDAO<Student> baseDao = new BaseDAO<Student>();
		string query = "INSERT INTO Student (StudentId, StudentName, InGameName, CharId) VALUES (@StudentId, @StudentName, @InGameName, @CharId)";
		Student student = new Student(studentId, studentName, inGameName, charId);
		int result = baseDao.ExecuteQuery(query, student);
		return result;

	}
	public bool CheckStudentExist(string studentId)
	{
		string query = "SELECT COUNT(1) FROM Student WHERE StudentId = @StudentId";
		BaseDAO<bool> baseDao = new BaseDAO<bool>();
		var studentObj = new { StudentId = studentId };
		bool exist = baseDao.ExecuteScalar(query, studentObj);
		return exist;
	}
	public bool CheckInGameNameExist(string inGameName)
	{
		string query = "SELECT COUNT(1) FROM Student WHERE InGameName = @InGameName";
		BaseDAO<bool> baseDao = new BaseDAO<bool>();
		var nameObj = new { InGameName = inGameName };
		bool exist = baseDao.ExecuteScalar(query, nameObj);
		
		return exist;
	}

}
