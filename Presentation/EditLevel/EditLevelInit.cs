using Godot;
using System;
using System.Collections.Generic;

public class EditLevelInit : Node2D
{
    static public int updated = 0;

    EditLevelBL editLevelBL;

    OptionButton timeLimitBtn;
    LineEdit levelNameLine;
    //OptionButton monsterIdBtn;
    Label errorMessageLabel;

    TextureButton arrowLeft, arrowRight, restoreOriginal;
    AnimatedSprite charSprite;
    List<string> animationList;

    string spritePath = "res://CharSprites/";
    CharacterBL characterBL;
    List<Monster> monsterList;
    string charPath;
    int numberOfChar;
    int count = 0;

    static string oldName;
    static int oldMonsterId;
    static int oldTimeLimit;

    int[] timeLimitOptions;
    //int[] monsterIdOptions;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        editLevelBL = new EditLevelBL();

        timeLimitOptions = new int[] { 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
        //monsterIdOptions = new int[] { 1, 2, 3, 4, 5 };

        timeLimitBtn = GetNode<OptionButton>("TimeLimit");
        levelNameLine = GetNode<LineEdit>("LevelName");
        //monsterIdBtn = GetNode<OptionButton>("MonsterId");
        errorMessageLabel = GetNode<Label>("ErrorMessageLabel");

        arrowLeft = GetNode<TextureButton>("MonsterSelect/ArrowLeft");
        arrowRight = GetNode<TextureButton>("MonsterSelect/ArrowRight");
        charSprite = GetNode<AnimatedSprite>("MonsterSelect/MonsterSprite");
        animationList = new List<string>();
        animationList.Add("Idle");

        restoreOriginal = GetNode<TextureButton>("RestoreOriginal");

        addOptions();

        //testing
        //Global.StudentId = 23;
        //Global.CustomLevelId = 10;
        //testing

        displayLevelInit();
    }

    /// <summary>
    /// Display original or updated level name, selected monster, selected time limit
    /// </summary>
    private void displayLevelInit()
    {
        if(updated == 0)
        {
            CustomLevel levelInfo = editLevelBL.loadCustomLevelInfo();
            levelNameLine.Text = levelInfo.CustomLevelName;

            int i = 0;
            foreach (int a in timeLimitOptions)
            {
                if (a == levelInfo.TimeLimit)
                {
                    GD.Print("Found time: " + a);
                    break;
                }
                i++;
            }
            timeLimitBtn.Select(i);

            GD.Print("\nCustom Level Id: " + levelInfo.CustomLevelId + "\nCustom Level Name: " + levelInfo.CustomLevelName + "\nMonster Id: " + levelInfo.Monster.MonsterId +
                "\nTimeLimit: " + levelInfo.TimeLimit);

            count = levelInfo.Monster.MonsterId - 1;
            changeArrowButtonStatues();
            displayCharacter();

            oldName = levelInfo.CustomLevelName;
            oldMonsterId = levelInfo.Monster.MonsterId;
            oldTimeLimit = levelInfo.TimeLimit;
        }
        else
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

            GD.Print("\nCustom Level Id: " + Global.CustomLevelId + "\nCustom Level Name: " + Global.CustomLevelName + "\nMonster Id: " + Global.MonsterId +
                "\nTimeLimit: " + Global.TimeLimit);

            count = Global.MonsterId - 1;
            changeArrowButtonStatues();
            displayCharacter();
        }
    }

    /// <summary>
    /// Change Monster to the one on the left
    /// </summary>
    private void _on_ArrowLeft_pressed()
    {
        count--;
        GD.Print("Count: " + count);
        changeArrowButtonStatues();
        displayCharacter();
    }

    /// <summary>
    ///  Change Monster to the one on the right
    /// </summary>
    private void _on_ArrowRight_pressed()
    {
        count++;
        GD.Print("Count: " + count);
        changeArrowButtonStatues();
        displayCharacter();
    }

    /// <summary>
    /// Display the corresponding Monster whenever the left or right arrows are pressed
    /// </summary>
    private void displayCharacter()
    {
        string name = monsterList[count].MonsterName;
        charPath = String.Format(spritePath + "{0}/", name);
        GD.Print(charPath);
        Global.LoadSprite(charPath, charSprite, animationList);
    }

    /// <summary>
    /// Insert available timelimit and Monster options
    /// </summary>
    private void addOptions()
    {
        //monster
        characterBL = new CharacterBL();
        monsterList = characterBL.GetAllMonsters();
        displayCharacter();

        numberOfChar = monsterList.Count;
        GD.Print("Number of monster: " + numberOfChar);

        changeArrowButtonStatues();

        //timeLimit
        foreach (int i in timeLimitOptions)
            timeLimitBtn.AddItem(i.ToString());
    }

    /// <summary>
    /// Change the status of the arrow buttons when one of them is pressed
    /// </summary>
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
    /// Checks if LevelName is valid before going to next step of edit level when the Next button is pressed
    /// </summary>
    private void _on_NextStepBtn_pressed()
    {

        string levelName = levelNameLine.Text;
        int monsterId = monsterList[count].MonsterId;
        int timeLimit = Int32.Parse(timeLimitBtn.Text);

        //GD.Print("Level Name: " + levelName + "\nMonsterId: " + monsterId + "\nTimeLimit: " + timeLimit);

        if (levelName == "")
        {
            GD.Print("Level name field is empty!");
            errorMessageLabel.SetText("Level name field is empty!");
        }
        else if (EditLevelBL.checkValidLevelName(oldName, levelName) != 1)
        {
            GD.Print("Level name already exist!");
            errorMessageLabel.SetText("Level name already exist!");
        }
        else
        {
            EditLevel.setLevelInitInfo(levelName, monsterId, timeLimit);
            GetTree().ChangeScene("res://Presentation/EditLevel/EditLevel.tscn");
        }
    }

    /// <summary>
    /// Restore Original LevelInit Info
    /// </summary>
    private void _on_RestoreOriginal_pressed()
    {
        levelNameLine.Text = oldName;

        int i = 0;
        foreach (int a in timeLimitOptions)
        {
            if (a == oldTimeLimit)
            {
                GD.Print("Found time: " + a);
                break;
            }
            i++;
        }
        timeLimitBtn.Select(i);

        count = oldMonsterId - 1;
        changeArrowButtonStatues();
        displayCharacter();
    }
}






