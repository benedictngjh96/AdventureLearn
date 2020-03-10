using Godot;
using System;

public class SimpleLogin : Node2D
{
	LineEdit nameLine;
	LineEdit emailLine;

	public override void _Ready()
	{
		nameLine = GetNode<LineEdit>("LoginFields/Name");
		emailLine = GetNode<LineEdit>("LoginFields/Email");
	}
	
	private void _on_LoginBtn_pressed()
	{
		string name = nameLine.Text;
		string email = emailLine.Text;

		string query = String.Format("SELECT StudentId FROM Student s WHERE StudentName = '{0}' AND StudentEmail = '{1}'", name, email);

		BaseDao<int> baseDao = new BaseDao<int>();
		int result = baseDao.RetrieveQuery(query);

		if(result == 0)
		{
			GD.Print("Invalid login credentials");
		}
		else
		{
			Global.StudentId = result;
			GD.Print("StudentId(" + Global.StudentId + ") login successful.");
			GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
		}
	}	
}


