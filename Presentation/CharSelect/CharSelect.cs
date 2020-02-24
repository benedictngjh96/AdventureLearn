using Godot;
using System;
using System.Collections.Generic;

public class CharSelect : Node2D
{
	StudentBL studentBL;
	int charId;

	AnimatedSprite charSprite;
	Button enterBtn;

	public override void _Ready()
	{
		//Hide animated sprite and button when user has not selected a character
		charSprite = GetNode<AnimatedSprite>("Char/AnimatedSprite");
		enterBtn = GetNode<Button>("EnterBtn");
		charSprite.Visible = false;
		enterBtn.Visible = false;

		studentBL = new StudentBL();
		
		//Redirect user if has existing account
		if (studentBL.CheckStudentExist(Global.StudentId))
		{
			GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
		}
		
		
	}

	private void _on_Knight1Button_pressed()
	{
		DisplayCharacter("Knight1");
	}
	private void _on_Knight2Button_pressed()
	{
		DisplayCharacter("Knight2");
	}
	private void DisplayCharacter(String characterName)
	{
		Label skillDescription = GetNode<Label>("Char/SkillDescription");

		charSprite.Play(characterName, false);
		charSprite.Visible = true;
		enterBtn.Visible = true;

		/*
		//Find character obj in characterlist using charactername
		//List<Character> characterList = studentBL.GetCharacterList();
		Character result = characterList.Find(item => item.CharName == characterName);
		skillDescription.Text = result.CharSkill;
		charId = result.CharId;
		*/
	}
  
	private void _on_EnterBtn_pressed()
	{
		LineEdit nameLine = GetNode<LineEdit>("InGameName/NameLine");
		Label errorLbl = GetNode<Label>("InGameName/ErrorLabel");
		string ign = nameLine.Text;
		bool exist = studentBL.CheckInGameNameExist(nameLine.Text);

		//Validation
		if (string.IsNullOrEmpty(ign))
		{
			errorLbl.Text = "Please enter ur in game name.";
		}
		else if (exist)
		{
			errorLbl.Text = "Name already exists.Choose another name";
		}
		else
		{
			//Redirect user to mainmenu if user record is successfully inserted
			//int result = studentBL.InsertStudent(Global.studentId, Global.studentName, ign, charId);
			/*
			if(result != 0)
			{
				GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
			}
			*/
		}
		
	}

}
