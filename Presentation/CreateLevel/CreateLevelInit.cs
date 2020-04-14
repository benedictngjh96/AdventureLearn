using Godot;
using System;
using System.Collections.Generic;

public class CreateLevelInit : Node2D
{
	OptionButton timeLimitBtn;
	LineEdit levelNameLine;
	//OptionButton monsterIdBtn;
	Label errorMessageLabel;

	TextureButton arrowLeft, arrowRight;
	AnimatedSprite charSprite;
	Label charName;
	List<string> animationList;

	string spritePath = "res://CharSprites/";
	CharacterBL characterBL;
	List<Character> characterList;
	string charPath;
	int numberOfChar;
	int count = 0;

	int[] timeLimitOptions;
	//int[] monsterIdOptions;

	/// <summary>
	/// Initialization
	/// </summary>
	/// <returns></returns>
	public override void _Ready()
	{
		timeLimitOptions = new int[] { 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
		//monsterIdOptions = new int[] { 1, 2, 3, 4, 5 };

		timeLimitBtn = GetNode<OptionButton>("TimeLimit");
		levelNameLine = GetNode<LineEdit>("LevelName");
		//monsterIdBtn = GetNode<OptionButton>("MonsterId");
		errorMessageLabel = GetNode<Label>("ErrorMessageLabel");

		arrowLeft = GetNode<TextureButton>("MonsterSelect/ArrowLeft");
		arrowRight = GetNode<TextureButton>("MonsterSelect/ArrowRight");
		charSprite = GetNode<AnimatedSprite>("MonsterSelect/MonsterSprite");
		charName = GetNode<Label>("MonsterSelect/MonsterName");
		animationList = new List<string>();
		animationList.Add("Idle");

		addOptions();
	}

	/// <summary>
	/// What happens whenever the left arrow in Select Monster is clicked
	/// </summary>
	/// <returns></returns>
	private void _on_ArrowLeft_pressed()
	{
		count--;
		changeArrowButtonStatues();
		displayCharacter();
	}

	/// <summary>
	/// What happens whenever the right arrow in Select Monster is clicked
	/// </summary>
	/// <returns></returns>
	private void _on_ArrowRight_pressed()
	{
		count++;
		changeArrowButtonStatues();
		displayCharacter();
	}

	/// <summary>
	/// Display the corresponding monster whenever the left or right arrows are clicked
	/// </summary>
	/// <returns></returns>
	private void displayCharacter()
	{
		string name = characterList[count].CharName;
		charPath = String.Format(spritePath + "{0}/", name);
		GD.Print(charPath);
		Global.LoadSprite(charPath, charSprite, animationList);
		charName.Text = name;
	}

	/// <summary>
	/// Insert available timelimit and monster options
	/// </summary>
	/// <returns></returns>
	private void addOptions()
	{
		//monster
		characterBL = new CharacterBL();
		characterList = characterBL.GetAllCharacters();
		displayCharacter();

		numberOfChar = characterList.Count;
		GD.Print("Number of characters: " + numberOfChar);

		GD.Print("CharName: " + characterList[count].CharName);

		changeArrowButtonStatues();

		//timeLimit
		foreach (int i in timeLimitOptions)
			timeLimitBtn.AddItem(i.ToString());
	}

	/// <summary>
	/// Change the status of the arrow buttons 
	/// </summary>
	/// <returns></returns>
	private void changeArrowButtonStatues()
	{
		if (count == 0)
		{
			arrowLeft.Disabled = true;
			arrowRight.Disabled = false;
		}
		else if (count == numberOfChar - 1)
		{
			arrowLeft.Disabled = false;
			arrowRight.Disabled = true;
		}
		else
		{
			arrowLeft.Disabled = false;
			arrowRight.Disabled = false;
		}
	}

	/// <summary>
	/// Go to next step of level creation
	/// </summary>
	/// <returns></returns>
	private void _on_NextStepBtn_pressed()
	{
		string levelName = levelNameLine.Text;
		//int monsterId = Int32.Parse(monsterIdBtn.Text);
		int monsterId = characterList[count].CharId;
		int timeLimit = Int32.Parse(timeLimitBtn.Text);

		//GD.Print("Level Name: " + levelName + "\nMonsterId: " + monsterId + "\nTimeLimit: " + timeLimit);

		//Global.StudentId = 1; //testing purposes, remember to remove
		GD.Print("\nStudent Id: " + Global.StudentId);

		if (levelName == "")
		{
			GD.Print("Level name field is empty!");
			errorMessageLabel.SetText("Level name field is empty!");
		}
		else if (CreateLevelBL.checkValidLevelName(levelName) != 1)
		{
			GD.Print("Level name already exist!");
			errorMessageLabel.SetText("Level name already exist!");
		}
		else
		{
			GetTree().ChangeScene("res://Presentation/CreateLevel/CreateLevel.tscn");
			CreateLevel.setLevelInitInfo(levelName, monsterId, timeLimit);
		}
	}
}

