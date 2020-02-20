using Godot;
using System;
using System.Collections.Generic;

public class StudentBL : Node
{
	StudentDao studentDao = new StudentDao();
	CharSelectDao charSelectDao = new CharSelectDao();
	public bool CheckStudentExist()
	{
		bool exist = studentDao.CheckStudentExist(Global.StudentId);
		return exist;
	}
	public bool CheckInGameNameExist(string ign)
	{
		bool exist = studentDao.CheckInGameNameExist(ign);
		return exist;
	}
	public List<Character> GetCharacterList()
	{
		List<Character> characterList = charSelectDao.GetAllCharacters();
		return characterList;
	}
	public int InsertStudent(string studentName, int charId, string studentEmail, string studentUsername, string studentPassword)
	{
		int result = studentDao.InsertStudent(studentName, charId, studentEmail, studentUsername, studentPassword);
		return result;
	}
	

}
