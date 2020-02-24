
using System;
using System.Collections.Generic;

public class StudentBL
{
	StudentDao studentDao = new StudentDao();
	public bool CheckStudentExist(int studentId)
	{
		bool exist = studentDao.CheckStudentExist(studentId);
		return exist;
	}
	public bool CheckInGameNameExist(string ign)
	{
		bool exist = studentDao.CheckInGameNameExist(ign);
		return exist;
	}
	
	public int InsertStudent(string studentName, int charId, string studentEmail, string studentUsername, string studentPassword)
	{
		int result = studentDao.InsertStudent(studentName, charId, studentEmail, studentUsername, studentPassword);
		return result;
	}
	

}
