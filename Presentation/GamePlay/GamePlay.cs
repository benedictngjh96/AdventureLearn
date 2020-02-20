using Godot;
using System;
using System.Collections.Generic;

public class GamePlay : Node2D
{
	GamePlayBL gamePlayBL;
	StudentScoreBL studentScoreBL;
	List<string> animationList;
	List<CampaignQuestion> campaignQuestionList;
	AnimatedSprite charSprite;
	AnimatedSprite monsterSprite;
	Question question;
	Character character;
	string monsterName;
	Label timerLabel;
	Label questionLabel;
	int questionIndex = 0;

	int s =0;
	public override void _Ready()
	{
		gamePlayBL = new GamePlayBL();
		studentScoreBL = new StudentScoreBL();
		charSprite = GetNode<AnimatedSprite>("CharSprite");
		monsterSprite = GetNode<AnimatedSprite>("MonsterSprite");
		timerLabel = GetNode<Label>("TimerLabel");
		questionLabel = GetNode<Label>("QuestionLabel");
		animationList = new List<string>();
		animationList.Add("Idle");
		animationList.Add("Hurt");
		animationList.Add("Die");
		animationList.Add("Attack");
		/*TODO
		switch (Global.LevelSelected)
		{
			case "Campaign":
				break;
			case "Student":
				break;
			case "Teacher":
				break;
			
		}
		*/
		Global.LevelSelected = "";
		
		/*
		//REMOVE 
		Global.WorldId = 1;
		Global.SectionId = 1;
		Global.LevelId = 1;
		Global.StudentId = 1;
		*/
		SetTimeLimit();

		GetMonsterName();

		LoadCharacter();
		var spritePath = "res://CharSprites/";
		var charPath = String.Format(spritePath + "{0}/", GetCharacterName());
		var monsterPath = String.Format(spritePath + "{0}/", GetMonsterName());

		LoadSprite(charPath, charSprite);
		LoadSprite(monsterPath, monsterSprite);
		LoadLevelQuestions();
		GD.Print("da3");

		DisplayQuestion();
		SetQuestionNum();
	}
	public override void _Process(float delta)
	{
		Timer timer = new Timer();
		timerLabel.Text = s.ToString();

		if (s <= 0)
			GetTree().ChangeScene("res://Presentation/World/World.tscn");
	}
	private void SetQuestionNum()
	{
		questionLabel.Text = String.Format("{0}/{1}", questionIndex + 1, campaignQuestionList.Count);
	}
	private void SetTimeLimit()
	{
		s = gamePlayBL.GetTimeLimit();
		
	}
	private void LoadLevelQuestions()
	{
		campaignQuestionList = gamePlayBL.GetLevelQuestions();
	}
	private void DisplayQuestion()
	{
		Control controlParentNode = GetNode<Control>("Buttons");
		List<string> optionList = new List<string>();
		Label questionTitle = GetNode<Label>("QuestionTitle");
		question = campaignQuestionList[questionIndex].Question;
		optionList.Add(question.Option1);
		optionList.Add(question.Option2);
		optionList.Add(question.Option3);
		optionList.Add(question.CorrectOption);
		questionTitle.Text = campaignQuestionList[questionIndex].Question.QuestionTitle;
		Random r = new Random();

		//Shuffle options
		foreach (Node n in controlParentNode.GetChildren())
		{
			if(n is Button)
			{
				Button optionButton = n as Button;
				int rng = r.Next(optionList.Count);

				optionButton.Text = optionList[rng];
				optionList.RemoveAt(rng);
			}
		}
	}
	private void LoadCharacter()
	{
		character = gamePlayBL.GetCharacter();
	}
	private string GetCharacterName()
	{
		return character.CharName;
	}
	private string GetMonsterName()
	{
		monsterName = gamePlayBL.GetMonsterName();
		return monsterName;
	}
	private void LoadSprite(string spritePath, AnimatedSprite animatedSprite)
	{
		SpriteFrames spriteFrames = new SpriteFrames();
		foreach (string animation in animationList)
		{
			var dir = new Directory();
			dir.Open(spritePath+animation);

			dir.ListDirBegin();
			var fileName = dir.GetNext();
			string strFileExtention = System.IO.Path.GetExtension(fileName);
			spriteFrames.AddAnimation(animation);
			int count = 0;

			while (!String.IsNullOrEmpty(fileName))
			{
				fileName = fileName.Replace(strFileExtention, "");
				var sprite = ResourceLoader.Load(spritePath + animation+"/"+fileName) as Texture;
				spriteFrames.AddFrame(animation, sprite);
				fileName = dir.GetNext();
				count++;
			}
			animatedSprite.Frames = spriteFrames;
			animatedSprite.SpeedScale = 2;
		}
		animatedSprite.Play("Idle");
	}
	private void DisplayNextQuestion()
	{
		if (questionIndex < campaignQuestionList.Count-1)
		{
			questionIndex++;
			DisplayQuestion();
			SetQuestionNum();
		}
		else
		{
			studentScoreBL.InsertStudentScore(s, gamePlayBL.GetTimeLimit());
			GetTree().ChangeScene("res://Presentation/World/World.tscn");
		}
	}
	private void CheckCorrectAnswer(string option)
	{
		if (option == question.CorrectOption)
		{
			charSprite.Play("Attack");
			monsterSprite.Play("Hurt");
			DisplayNextQuestion();
		}
		else
		{
			monsterSprite.Play("Attack");
			charSprite.Play("Hurt");
			s -= 10;
		}
	}
	private void _on_Option1_pressed()
	{
		Button option = GetNode<Button>("Buttons/Option1");
		CheckCorrectAnswer(option.Text);
	}

	private void _on_Option2_pressed()
	{
		Button option = GetNode<Button>("Buttons/Option2");
		CheckCorrectAnswer(option.Text);
	}

	private void _on_Option3_pressed()
	{
		Button option = GetNode<Button>("Buttons/Option3");
		CheckCorrectAnswer(option.Text);
	}

	private void _on_Option4_pressed()
	{
		Button option = GetNode<Button>("Buttons/Option4");
		CheckCorrectAnswer(option.Text);
	}

	private void _on_CharSprite_animation_finished()
	{
		if (charSprite.IsPlaying() && charSprite.Animation != "Idle")
			charSprite.Play("Idle");
	}

	private void _on_MonsterSprite_animation_finished()
	{
		if (monsterSprite.IsPlaying() && monsterSprite.Animation != "Idle")
			monsterSprite.Play("Idle");
	}

	private void _on_Timer_timeout()
	{
		s -= 1;
	}

}








