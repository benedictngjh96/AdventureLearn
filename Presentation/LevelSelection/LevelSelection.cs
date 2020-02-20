using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelSelection : Node2D
{
	//Asset path
	string unlockedLvl = "res://Assets/LevelSelection/redround.png";
	string lockedLvl = "res://Assets/LevelSelection/roundlocked.png";
	string starsImagePath = "res://Assets/Level/";

	SectionBL sectionBL;
	List<Section> sectionList;
	StudentScoreBL studentScoreBL;
	Sprite forwardSprite;
	TextureButton forwardBtn;
	Sprite backwardSprite;
	TextureButton backWardBtn;
	Label sectionLbl;
	int count = 1;

	public override void _Ready()
	{
		//Intializing nodes
		sectionBL = new SectionBL();
		studentScoreBL = new StudentScoreBL();
		forwardSprite = GetNode<Sprite>("ArrowNavigation/Forward");
		forwardBtn = GetNode<TextureButton>("ArrowNavigation/Forward/ForwardBtn");
		backwardSprite = GetNode<Sprite>("ArrowNavigation/Backward");
		backWardBtn = GetNode<TextureButton>("ArrowNavigation/Backward/BackwardBtn");
		sectionLbl = GetNode<Label>("Title/SectionName");

		//REMOVE
		Global.WorldId = 1;
		//REMOVE 
		Global.StudentId = 1;


		//On load set section id to 1 to display section 1
		Global.SectionId = 1;
		sectionList = sectionBL.GetWorldSections();
		sectionLbl.Text = sectionList[count - 1].SectionName;

		//Star path
		string[] starArray = new string[3];
		starArray[0] = "Level/1 stars.png";
		starArray[1] = "Level/2 stars.png";
		starArray[2] = "Level/3 stars.png";

		//Disable forward n back btns if only have 1 level
		DisableBothButtons();
		DisableBackwardButton();
		HideLevels();
		DisplayLevelScore();

	}
	private void HideLevels()
	{
		Section section = sectionBL.DisplaySectionLevels();
		Node levelNode = GetNode<Node>("Levels");
		foreach (Node n in levelNode.GetChildren())
		{
			if (n is Sprite)
			{
				Sprite levelSprite = (Sprite)n;
				levelSprite.SetVisible(false);
				foreach (Node no in levelSprite.GetChildren())
				{
					if (no is Sprite)
					{
						Sprite starSprite = (Sprite)no;
						starSprite.SetVisible(false);
					}
					if (no is TextureButton)
					{
						TextureButton textureButton = (TextureButton)no;
						textureButton.Disabled = true;
					}
				}
			}
		}
	}
	private void DisplayLevelScore()
	{
		string starPath;
		int index = 1;
		Node levelNode = GetNode<Node>("Levels");
		TextureButton textureBtn;
		var unlockedLevelTexture = ResourceLoader.Load(unlockedLvl) as Texture;
		var lockedLevelTexture = ResourceLoader.Load(lockedLvl) as Texture;

		//If student has not cleared a single level in the section
		if (studentScoreBL.GetStudentScores() == null)
		{
			Node level;
			Sprite levelSprite;
			Section section = sectionBL.GetSectionLevels();

			if(Global.SectionId == 1)
			{
				UnlockFirstLevel();

				for (int i = 1; i < section.Level.Count(); i++)
				{
					level = levelNode.GetChild(i);
					levelSprite = level as Sprite;
					levelSprite.SetVisible(true);
					levelSprite.Texture = lockedLevelTexture;
				}

			}
			else
			{
				//Check if previous section is cleared
				for (int i = 0; i < section.Level.Count(); i++)
				{
					level = levelNode.GetChild(i);
					levelSprite = level as Sprite;
					levelSprite.SetVisible(true);
					levelSprite.Texture = lockedLevelTexture;
				}
			}
			
			/*
			//Level 1 is always unlocked
			level = levelNode.GetChild(0);
			levelSprite = level as Sprite;
			levelSprite.Texture = unlockedLevelTexture;
			levelSprite.SetVisible(true);
			

			foreach(Node n in levelSprite.GetChildren())
			{
				if (n is TextureButton)
				{	
					textureBtn = n as TextureButton;
					textureBtn.Disabled = false;
					break;
				}
					
			}
			*/
			//Locked remaining levels
			/*
			for (int i = 0; i < section.Level.Count(); i++)
			{
				level = levelNode.GetChild(i);
				levelSprite = level as Sprite;
				levelSprite.SetVisible(true);
				levelSprite.Texture = lockedLevelTexture;
			}
			*/
		}
		else
		{
			Student student = studentScoreBL.GetStudentScores();
			int totalLevels = GetNode("Levels").GetChildCount();
			Section section = sectionBL.GetSectionLevels();
			Node level;
			Sprite levelSprite;
			int i = 0;
			Node levelParent = GetNode("Levels");

			//Display all section's levels
			foreach (Level lvl in section.Level)
			{
				level = levelNode.GetChild(i);
				levelSprite = level as Sprite;
				levelSprite.SetVisible(true);
				levelSprite.Texture = lockedLevelTexture;
				i++;
			}

			//Load student scores
			int count = 0;
			foreach (StudentScore score in student.StudentScore)
			{
				Node n = levelParent.GetChild(count);
				Sprite lvlNode = n as Sprite;
				lvlNode.Texture = unlockedLevelTexture;
				Sprite starNode = null;
				string starAssetPath = starsImagePath;
				foreach (Node node in lvlNode.GetChildren())
				{
					if (node is Sprite)
					{
						starNode = node as Sprite;
					}
					if (node is TextureButton)
					{
						textureBtn = node as TextureButton;
						textureBtn.Disabled = false;
					}
				}
				
				switch (score.LevelScore)
				{
					case int ls when (ls >= 1 && ls <= 50):
						starAssetPath += "1 stars.png";
						break;
					case int ls when (ls >= 51 && ls <= 70):
						starAssetPath += "2 stars.png";
						break;
					case int ls when (ls >= 71 && ls <= 100):
						starAssetPath += "3 stars.png";
						break;
				}
				var texture = ResourceLoader.Load(starAssetPath) as Texture;
				starNode.Texture = texture;
				starNode.SetVisible(true);

				count++;
			}
			//Unlock nxt lvl
			Node next = levelParent.GetChild(count);
			Sprite nextLvlNode = next as Sprite;
			nextLvlNode.Texture = unlockedLevelTexture;
			foreach(Node no in nextLvlNode.GetChildren())
			{
				if(no is TextureButton)
				{
					TextureButton textBtn = no as TextureButton;
					textBtn.Disabled = false;
				}
			}
		}
	}
	private void CheckSectionCleared()
	{
		int result = sectionBL.CheckSectionCleared();

		if(result == 0)
		{
			UnlockFirstLevel();

		}
	}
	private void UnlockFirstLevel()
	{
		Node levelNode = GetNode<Node>("Levels");
		TextureButton textureBtn;
		Node level;
		Sprite levelSprite;

		var unlockedLevelTexture = ResourceLoader.Load(unlockedLvl) as Texture;
		level = levelNode.GetChild(0);
		levelSprite = level as Sprite;
		levelSprite.Texture = unlockedLevelTexture;
		levelSprite.SetVisible(true);

		foreach (Node n in levelSprite.GetChildren())
		{
			if (n is TextureButton)
			{
				textureBtn = n as TextureButton;
				textureBtn.Disabled = false;
				break;
			}

		}
	}
	private void _on_ForwardBtn_pressed()
	{
		count++;
		if (count >= sectionList.Count())
			DisableForwardButton();
		if (backWardBtn.Disabled == true)
			EnableBackwardButton();
		Global.SectionId++;
		DisplaySectionName();
		HideLevels();
		DisplayLevelScore();
		CheckSectionCleared();
	}
	private void _on_BackwardBtn_pressed()
	{
		count--;
		if (count <= 1)
			DisableBackwardButton();
		if (forwardBtn.Disabled == true)
			EnableForwardButton();
		Global.SectionId--;
		DisplaySectionName();
		HideLevels();
		DisplayLevelScore();
		CheckSectionCleared();
	}

	private void DisplaySectionName()
	{
		sectionLbl.Text = sectionList[count - 1].SectionName;
	}
	private void EnableForwardButton()
	{
		forwardSprite.Modulate = new Color(255, 255, 255);
		forwardBtn.Disabled = false;
	}
	private void EnableBackwardButton()
	{
		backwardSprite.Modulate = new Color(255, 255, 255);
		backWardBtn.Disabled = false;
	}
	private void DisableForwardButton()
	{
		forwardSprite.Modulate = new Color(0, 0, 0);
		forwardBtn.Disabled = true;
	}
	private void DisableBackwardButton()
	{
		backwardSprite.Modulate = new Color(0, 0, 0);
		backWardBtn.Disabled = true;
	}
	private void DisableBothButtons()
	{
		if (sectionList.Count == 1)
		{
			DisableForwardButton();
			DisableBackwardButton();
		}
	}
	private void RedirectGamePlay()
	{
		GetTree().ChangeScene("res://Presentation/GamePlay/GamePlay.tscn");

	}
	//Buttons signal
	private void _on_Level1Btn_pressed()
	{
		RedirectGamePlay();
		Global.LevelId = 1;
	}
	private void _on_Level2Btn_pressed()
	{
		RedirectGamePlay();
		Global.LevelId = 2;
	}
	private void _on_Level3Btn_pressed()
	{
		RedirectGamePlay();
		Global.LevelId = 3;
	}
	private void _on_Level4Btn_pressed()
	{
		RedirectGamePlay();
		Global.LevelId = 4;
	}
	private void _on_Level5Btn_pressed()
	{
		RedirectGamePlay();
		Global.LevelId = 5;
	}
	private void _on_Level6Btn_pressed()
	{
		RedirectGamePlay();
		Global.LevelId = 6;
	}



}
