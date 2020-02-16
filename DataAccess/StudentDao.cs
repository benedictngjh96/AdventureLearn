using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Godot;
public class StudentDao
{
	public void InsertStudent(string studentId, string studentName, string inGameName, int charId)
	{
		string query = "INSERT INTO Student (StudentId, StudentName, InGameName, CharId) VALUES (@StudentId, @StudentName, @InGameName, @CharId)";
		try
		{
			using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
			{
				Student student = new Student(studentId, studentName, inGameName, charId);
				conn.Execute(query, student);
			}
		}
		catch(Exception ex)
		{
			GD.Print(ex.Message);
		}

	}
	public bool CheckStudentExist(string studentId)
	{
		bool exist = false;
		string query = "SELECT COUNT(1) FROM Student WHERE StudentId = @StudentId";
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			exist = conn.ExecuteScalar<bool>(query, new {  StudentId = studentId });
		}
		return exist;
	}

}
