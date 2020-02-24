using Godot;
using System;
using System.Collections.Generic;

public class GamePlay : Node2D
{
	List<string> animationList;
	List<Question> questionList;
	AnimatedSprite charSprite;
	AnimatedSprite monsterSprite;
	Question question;
	Label timerLabel;
	Label questionLabel;
	Label levelTitle;
	Button option1;
	Button option2;
	Button option3;
	Button option4;

	int questionIndex = 0;
	string spritePath = "res://CharSprites/";
	int timeLimit = 0;
	int s =0;


	public override void _Ready()
	{
		AddUserSignal("NoMoreQuestions");

		charSprite = GetNode<AnimatedSprite>("CharSprite");
		monsterSprite = GetNode<AnimatedSprite>("MonsterSprite");
		timerLabel = GetNode<Label>("TimerLabel");
		questionLabel = GetNode<Label>("QuestionLabel");
		levelTitle = GetNode<Label>("LevelTitle");
		option1 = GetNode<Button>("Buttons/Option1");
		option2 = GetNode<Button>("Buttons/Option2");
		option3 = GetNode<Button>("Buttons/Option3");
		option4 = GetNode<Button>("Buttons/Option4");

		animationList = new List<string>();
		animationList.Add("Idle");
		animationList.Add("Hurt");
		animationList.Add("Die");
		animationList.Add("Attack");
	}
	public override void _Process(float delta)
	{
		Timer timer = new Timer();
		timerLabel.Text = s.ToString();

		if (s <= 0)
			GetTree().ChangeScene("res://Presentation/World/World.tscn");
	}
	public void SetLevelTitle(string title)
	{
		levelTitle.Text = title;
	}
	public void SetQuestionNum()
	{
		questionLabel.Text = String.Format("{0}/{1}", questionIndex + 1, questionList.Count);
	}
	public void SetTimeLimit(int timeLimit)
	{
		s = timeLimit;
		this.timeLimit = timeLimit;
	}
	public void SetQuestionList(List<Question> questionList)
	{
		this.questionList = questionList;
	}
	public void DisplayQuestion()
	{
		Control controlParentNode = GetNode<Control>("Buttons");
		List<string> optionList = new List<string>();
		Label questionTitle = GetNode<Label>("QuestionTitle");
		question = questionList[questionIndex];
		optionList.Add(question.Option1);
		optionList.Add(question.Option2);
		optionList.Add(question.Option3);
		optionList.Add(question.CorrectOption);
		questionTitle.Text = questionList[questionIndex].QuestionTitle;
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

	public void LoadSprite(string spritePath, AnimatedSprite animatedSprite)
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
	public void DisplayCharSprite(Character character)
	{
		string charPath = String.Format(spritePath + "{0}/", character.CharName);
		LoadSprite(charPath, charSprite);
	}
	public void DisplayMonsterSprite(Monster monster)
	{
		string monsterPath = String.Format(spritePath + "{0}/", monster.MonsterName);
		LoadSprite(monsterPath, monsterSprite);
	}
	public void DisplayNextQuestion()
	{
		if (questionIndex < questionList.Count-1)
		{
			questionIndex++;
			DisplayQuestion();
			SetQuestionNum();
		}
		else
		{
			EmitSignal("NoMoreQuestions");
			GetTree().ChangeScene("res://Presentation/World/World.tscn");
		}
	}
	public void CheckCorrectAnswer(string option)
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
	public int GetTimeLeft()
	{
		return s;
	}
	private void _on_Option1_pressed()
	{
		CheckCorrectAnswer(option1.Text);
	}

	private void _on_Option2_pressed()
	{
		CheckCorrectAnswer(option2.Text);
	}

	private void _on_Option3_pressed()
	{
		CheckCorrectAnswer(option3.Text);
	}

	private void _on_Option4_pressed()
	{
		CheckCorrectAnswer(option4.Text);
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








