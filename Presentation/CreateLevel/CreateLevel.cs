using Godot;
using System;
using System.Text.RegularExpressions;

public class CreateLevel : Node2D
{
    CreateLevelBL createLevelBL;

    Label questionNumberLabel;
    Label errorMessageLabel;

    CheckBox checkbox1;
    CheckBox checkbox2;
    CheckBox checkbox3;
    CheckBox checkbox4;

    TextureButton question1Btn;
    TextureButton question2Btn;
    TextureButton question3Btn;
    TextureButton question4Btn;
    TextureButton question5Btn;

    ButtonGroup questionGroup;
    ButtonGroup checkboxGroup;

    LineEdit questionTitleLine;

    LineEdit option1Line;
    LineEdit option2Line;
    LineEdit option3Line;
    LineEdit option4Line;

    static string levelName;
    static int monsterId;
    static int timeLimit;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        createLevelBL = new CreateLevelBL();

        questionNumberLabel = GetNode<Label>("QuestionNumberLabel");
        errorMessageLabel = GetNode<Label>("ErrorMessageLabel");

        checkbox1 = GetNode<CheckBox>("Options/Option1/CheckBox1");
        checkbox2 = GetNode<CheckBox>("Options/Option2/CheckBox2");
        checkbox3 = GetNode<CheckBox>("Options/Option3/CheckBox3");
        checkbox4 = GetNode<CheckBox>("Options/Option4/CheckBox4");

        question1Btn = GetNode<TextureButton>("QuestionSelect/Question1");
        question2Btn = GetNode<TextureButton>("QuestionSelect/Question2");
        question3Btn = GetNode<TextureButton>("QuestionSelect/Question3");
        question4Btn = GetNode<TextureButton>("QuestionSelect/Question4");
        question5Btn = GetNode<TextureButton>("QuestionSelect/Question5");

        questionGroup = new ButtonGroup();
        checkboxGroup = new ButtonGroup();

        question1Btn.SetButtonGroup(questionGroup);
        question2Btn.SetButtonGroup(questionGroup);
        question3Btn.SetButtonGroup(questionGroup);
        question4Btn.SetButtonGroup(questionGroup);
        question5Btn.SetButtonGroup(questionGroup);

        checkbox1.SetButtonGroup(checkboxGroup);
        checkbox2.SetButtonGroup(checkboxGroup);
        checkbox3.SetButtonGroup(checkboxGroup);
        checkbox4.SetButtonGroup(checkboxGroup);

        checkbox1.SetPressed(true);

        questionTitleLine = GetNode<LineEdit>("QuestionTitle");

        option1Line = GetNode<LineEdit>("Options/Option1");
        option2Line = GetNode<LineEdit>("Options/Option2");
        option3Line = GetNode<LineEdit>("Options/Option3");
        option4Line = GetNode<LineEdit>("Options/Option4");

        if(CreateLevelInit.updated == 0)
        {
            createLevelBL.initializeQuestions();
        }
        else
        {
            createLevelBL.reloadTempQuestionList();
            displayQuestion();
        }
    }

    /// <summary>
    /// Get levelName, monsterId, and timeLimit from CreateLevelInit
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="time"></param>
    public static void setLevelInitInfo(string name, int id, int time)
    {
        levelName = name;
        monsterId = id;
        timeLimit = time;

        GD.Print("Level Name: " + levelName + "\nMonster Id: " + monsterId + "\nTime Limit: " + timeLimit);
    }

    /// <summary>
    /// Validates the user created questions through business logic before inserting them into database when the Create button is pressed
    /// </summary>
    private void _on_CreateBtn_pressed()
    {
        saveQuestion();
        if (findEmptyFields() == 0 && findDuplicateOptions() == 0)
        {
            GD.Print("Start inserting into database.");
            createLevelBL.createLevel(levelName, monsterId, timeLimit);
            CreateLevelInit.updated = 0;
            GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
        }
    }

    /// <summary>
    /// Saves a single user created question
    /// </summary>
    private void saveQuestion()
    {
        string option1 = option1Line.Text;
        string option2 = option2Line.Text;
        string option3 = option3Line.Text;
        string option4 = option4Line.Text;

        int correctOption = Int32.Parse(Regex.Match(checkboxGroup.GetPressedButton().Name, @"\d+").Value);

        /*GD.Print("\nQuestion Number:" + getQuestionNumber() +"\nQuestion Title: " + questionTitleLine.Text + "\nOption 1: " + option1 + "\nOption 2: " + option2 + 
            "\nOption 3: " + option3 + "\nOption 4: " + option4 + "\nCorrect Option: " + correctOption);*/

        createLevelBL.saveQuestion((getQuestionNumber() - 1), option1, option2, option3, option4, correctOption, questionTitleLine.Text); //questionNumber - 1 = questionId
    }

    /// <summary>
    /// Get Question number
    /// </summary>
    /// <returns>Return acquired Question number</returns>
    private int getQuestionNumber()
    {
        int questionNumber = Int32.Parse(Regex.Match(questionNumberLabel.Text, @"\d+").Value);

        switch (questionNumber)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 3;
            case 4:
                return 4;
            case 5:
                return 5;
            default: return 0;
        }
    }

    /// <summary>
    /// Load question 1 and saves previous question
    /// </summary>
    private void _on_Question1_pressed()
    {
        saveQuestion();

        questionNumberLabel.SetText("Enter Question 1:");
        displayQuestion();
    }

    /// <summary>
    /// Load question 2 and saves previous question
    /// </summary>
    private void _on_Question2_pressed()
    {
        saveQuestion();

        questionNumberLabel.SetText("Enter Question 2:");
        displayQuestion();
    }

    /// <summary>
    /// Load question 3 and saves previous question
    /// </summary>
    private void _on_Question3_pressed()
    {
        saveQuestion();

        questionNumberLabel.SetText("Enter Question 3:");
        displayQuestion();
    }

    /// <summary>
    /// Load question 4 and saves previous question
    /// </summary>
    private void _on_Question4_pressed()
    {
        saveQuestion();

        questionNumberLabel.SetText("Enter Question 4:");
        displayQuestion();
    }

    /// <summary>
    /// Load question 5 and saves previous question
    /// </summary>
    private void _on_Question5_pressed()
    {
        saveQuestion();

        questionNumberLabel.SetText("Enter Question 5:");
        displayQuestion();
    }

    /// <summary>
    /// Display corresponding question
    /// </summary>
    private void displayQuestion()
    {
        UserCreatedQuestion q = createLevelBL.GetQuestion(getQuestionNumber());

        questionTitleLine.SetText(q.QuestionTitle);
        option1Line.SetText(q.Option1);
        option2Line.SetText(q.Option2);
        option3Line.SetText(q.Option3);
        option4Line.SetText(q.Option4);

        switch (q.CorrectOption)
        {
            case 1:
                checkbox1.SetPressed(true);
                break;
            case 2:
                checkbox2.SetPressed(true);
                break;
            case 3:
                checkbox3.SetPressed(true);
                break;
            case 4:
                checkbox4.SetPressed(true);
                break;
        }
    }

    /// <summary>
    /// Find empty fields, and direct users there
    /// </summary>
    /// <returns>Return 1 if an empty field is found, else return 0</returns>
    private int findEmptyFields()
    {
        /*if (questionTitleLine.Text == "" || option1Line.Text == "" || option2Line.Text == "" || option3Line.Text == "" || option4Line.Text == "")
            return true;*/
        int questionWithMissingFields = createLevelBL.checkEmptyFieldsExist();

        if (questionWithMissingFields != -1)
        {
            errorMessageLabel.SetText("Question " + questionWithMissingFields + " has empty fields!");

            switch (questionWithMissingFields)
            {
                case 1:
                    questionNumberLabel.SetText("Enter Question 1:");
                    displayQuestion();
                    break;
                case 2:
                    questionNumberLabel.SetText("Enter Question 2:");
                    displayQuestion();
                    break;
                case 3:
                    questionNumberLabel.SetText("Enter Question 3:");
                    displayQuestion();
                    break;
                case 4:
                    questionNumberLabel.SetText("Enter Question 4:");
                    displayQuestion();
                    break;
                case 5:
                    questionNumberLabel.SetText("Enter Question 5:");
                    displayQuestion();
                    break;

            }
            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Find a question with duplicated options, and direct users there
    /// </summary>
    /// <returns>Return 1 if an empty field is found, else return 0</returns>
    private int findDuplicateOptions()
    {
        int questionWithDuplicateOptions = createLevelBL.checkDuplicationOptions();

        if (questionWithDuplicateOptions != -1)
        {
            errorMessageLabel.SetText("Question " + questionWithDuplicateOptions + " has duplicate options!");

            switch (questionWithDuplicateOptions)
            {
                case 1:
                    questionNumberLabel.SetText("Enter Question 1:");
                    displayQuestion();
                    break;
                case 2:
                    questionNumberLabel.SetText("Enter Question 2:");
                    displayQuestion();
                    break;
                case 3:
                    questionNumberLabel.SetText("Enter Question 3:");
                    displayQuestion();
                    break;
                case 4:
                    questionNumberLabel.SetText("Enter Question 4:");
                    displayQuestion();
                    break;
                case 5:
                    questionNumberLabel.SetText("Enter Question 5:");
                    displayQuestion();
                    break;

            }

            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Return to CreateLevelInit Scene
    /// </summary>
    private void _on_BackBtn_pressed()
    {
        saveQuestion();
        Global.QuestionList = createLevelBL.getTempQuestionList();
        Global.CustomLevelName = levelName;
        Global.MonsterId = monsterId;
        Global.TimeLimit = timeLimit;
        CreateLevelInit.updated = 1;

        GetTree().ChangeScene("res://Presentation/CreateLevel/CreateLevelInit.tscn");
    }
}


