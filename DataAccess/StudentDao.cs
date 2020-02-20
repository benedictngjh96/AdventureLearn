using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Godot;
public class StudentDao
{
	
	public int InsertStudent(string studentName, int charId, string studentEmail, string studentUsername, string studentPassword)
	{
		BaseDao<Student> baseDao = new BaseDao<Student>();
		string query = "INSERT INTO Student (StudentName, CharId, StudentEmail, StudentUsername, StudentPassword) VALUES (@StudentName, @CharId, @StudentEmail, @StudentUsername, StudentPassword)";
		Student student = new Student(studentName, charId, studentEmail, studentUsername, studentPassword);
		int result = baseDao.ExecuteQuery(query, student);
		return result;

	}
	public bool CheckStudentExist(int studentId)
	{
		string query = "SELECT COUNT(1) FROM Student WHERE StudentId = @StudentId";
		BaseDao<bool> baseDao = new BaseDao<bool>();
		var studentObj = new { StudentId = studentId };
		bool exist = baseDao.ExecuteScalar(query, studentObj);
		return exist;
	}
	public bool CheckInGameNameExist(string inGameName)
	{
		string query = "SELECT COUNT(1) FROM Student WHERE InGameName = @InGameName";
		BaseDao<bool> baseDao = new BaseDao<bool>();
		var nameObj = new { InGameName = inGameName };
		bool exist = baseDao.ExecuteScalar(query, nameObj);
		
		return exist;
	}

}
