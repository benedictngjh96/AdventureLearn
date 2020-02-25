using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
public class StudentDao
{
	/// <summary>
	/// Return int result 1 if InsertStudent has executed successfully
	/// </summary>
	/// <param name="studentName"></param>
	/// <param name="charId"></param>
	/// <param name="studentEmail"></param>
	/// <param name="studentUsername"></param>
	/// <param name="studentPassword"></param>
	/// <returns></returns>
	public int InsertStudent(string studentName, int charId, string studentEmail, string studentUsername, string studentPassword, string hash, string salt)
	{
		BaseDao<Student> baseDao = new BaseDao<Student>();
		string query = "INSERT INTO Student (StudentName, CharId, StudentEmail, StudentUsername, StudentPassword, Hash, Salt) " +
			"VALUES (@StudentName, @CharId, @StudentEmail, @StudentUsername, @StudentPassword, @Hash, @Salt)";
		Student student = new Student(studentName, charId, studentEmail, studentUsername, studentPassword);
		int result = baseDao.ExecuteQuery(query, new { StudentName = studentName, CharId = charId, StudentEmail = studentEmail, StudentUsername = studentUsername, 
			StudentPassword = studentPassword, Hash = hash, Salt = salt });
		return result;

	}
	/// <summary>
	/// Return int value 1 if userName and password is valid
	/// </summary>
	/// <param name="userName"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	public int CheckStudentLogin(string userName, string password)
	{
		return 1;
	}
	/// <summary>
	/// Return true if student exist
	/// </summary>
	/// <param name="studentId"></param>
	/// <returns></returns>
	public bool CheckStudentExist(int studentId)
	{
		string query = "SELECT COUNT(1) FROM Student WHERE StudentId = @StudentId";
		BaseDao<bool> baseDao = new BaseDao<bool>();
		var studentObj = new { StudentId = studentId };
		bool exist = baseDao.ExecuteScalar(query, studentObj);
		return exist;
	}
	public Student GetStudent()
	{
		BaseDao<Student> baseDao = new BaseDao<Student>();
		Student student = baseDao.RetrieveQuery("SELECT * FROM Student WHERE StudentId = 4");
		return student;
	}
}
