using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Presentation for CreateLevelInit
/// </summary>
public class CreateLevelInit : Node2D
{
    static public int updated = 0;

    OptionButton timeLimitBtn;
    LineEdit levelNameLine;
    Label errorMessageLabel;
    TextureButton arrowLeft, arrowRight;
    AnimatedSprite charSprite;
    List<string> animationList;

    string spritePath = "res://CharSprites/";
    CharacterBL characterBL;
    List<Monster> monsterList;
    string charPath;
    int numberOfChar;
    int count = 0;

    int[] timeLimitOptions;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        timeLimitOptions = new int[] { 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
        //monsterIdOptions = new int[] { 1, 2, 3, 4, 5 };

        timeLimitBtn = GetNode<OptionButton>("TimeLimitSelect/TimeLimit");
        levelNameLine = GetNode<LineEdit>("LevelNameSet/LevelName");
        //monsterIdBtn = GetNode<OptionButton>("MonsterId");
        errorMessageLabel = GetNode<Label>("ErrorMessageLabel");

        arrowLeft = GetNode<TextureButton>("MonsterSelect/ArrowLeft");
        arrowRight = GetNode<TextureButton>("MonsterSelect/ArrowRight");
        charSprite = GetNode<AnimatedSprite>("MonsterSelect/MonsterSprite");
        animationList = new List<string>();
        animationList.Add("Idle");
        
        //testing
        Global.StudentId = 37;
        //testing

        AddOptions();

        if(updated == 1)
        {
            DisplayLevelInit();
        }
    }

    /// <summary>
    /// Display the previously typed LevelInit Info
    /// </summary>
    private void DisplayLevelInit()
    {
        levelNameLine.Text = Global.CustomLevelName;

        int i = 0;
        foreach (int a in timeLimitOptions)
        {
            if (a == Global.TimeLimit)
            {
                GD.Print("Found time: " + a);
                break;
            }
            i++;
        }
        timeLimitBtn.Select(i);

        count = Global.MonsterId - 1;
        UpdateArrowButtonStatuses();
        DisplayCharacter();
    }

    /// <summary>
    /// Handles the logic when the Left button is pressed
    /// </summary>
    private void _on_ArrowLeft_pressed()
    {
        count--;
        UpdateArrowButtonStatuses();
        DisplayCharacter();
    }

    /// <summary>
    /// Handles the logic when the Right button is pressed
    /// </summary>
    private void _on_ArrowRight_pressed()
    {
        count++;
        UpdateArrowButtonStatuses();
        DisplayCharacter();
    }

    /// <summary>
    /// Display the corresponding Monster whenever the left or right arrows are pressed
    /// </summary>
    private void DisplayCharacter()
    {
        string name = monsterList[count].MonsterName;
        charPath = String.Format(spritePath + "{0}/", name);
        GD.Print(charPath);
        Global.LoadSprite(charPath, charSprite, animationList);
    }

    /// <summary>
    /// Insert available timelimit and Monster options
    /// </summary>
    private void AddOptions()
    {
        //monster
        characterBL = new CharacterBL();
        monsterList = characterBL.GetAllMonsters();
        DisplayCharacter();

        numberOfChar = monsterList.Count;
        GD.Print("Number of monsters: " + numberOfChar);

        UpdateArrowButtonStatuses();

        //timeLimit
        foreach (int i in timeLimitOptions)
            timeLimitBtn.AddItem(i.ToString());
    }

    /// <summary>
    /// Change the status of the Arrow buttons
    /// </summary>
    private void UpdateArrowButtonStatuses()
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
    /// Handles the logic when the Next button is pressed
    /// </summary>
    private void _on_NextBtn_pressed()
    {
        string levelName = levelNameLine.Text;
        //int monsterId = Int32.Parse(monsterIdBtn.Text);
        int monsterId = monsterList[count].MonsterId;
        int timeLimit = Int32.Parse(timeLimitBtn.Text);

        //GD.Print("Level Name: " + levelName + "\nMonsterId: " + monsterId + "\nTimeLimit: " + timeLimit);

        //testing
        //Global.StudentId = 1; 
        //GD.Print("\nStudent Id: " + Global.StudentId);
        //testing

        if (levelName == "")
        {
            GD.Print("Level name field is empty!");
            errorMessageLabel.SetText("Level name field is empty!");
        }
        else if (CreateLevelBL.CheckValidLevelName(levelName) != 1)
        {
            GD.Print("Level name already exist!");
            errorMessageLabel.SetText("Level name already exist!");
        }
        else
        {
            GetTree().ChangeScene("res://Presentation/CreateLevel/CreateLevel.tscn");
            CreateLevel.SetLevelInitInfo(levelName, monsterId, timeLimit);
        }
    }

    /// <summary>
    /// Handles the logic when the Back button is pressed
    /// </summary>
    private void _on_BackBtn_pressed()
    {
        updated = 0;
        GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
    }
}






