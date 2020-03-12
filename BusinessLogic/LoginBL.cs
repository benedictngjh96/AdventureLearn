using Godot;
using System;

public class LoginBL : Node
{
	StudentDao studentDao = new StudentDao();

	public void InsertStudent()
	{
		string userId = "test123";
		//Check if user exist
		//int result = studentDao.CheckStudentExist(userId);
		//Insert into db if user does not exist
		/*
		if(result != 1)
		{
			studentDao.InsertStudent(userId);
		}
		*/
	}
}
