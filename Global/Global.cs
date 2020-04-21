using System;
using Godot;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
public class Global : Node
{
    /// <summary>
    /// Global variables
    /// </summary>
    public static string StudentName { get; set; }
    public static int StudentId { get; set; }
    public static int WorldId { get; set; }
    public static int SectionId { get; set; }
    public static int LevelId { get; set; }
    public static int CustomLevelId { get; set; }
    public static int AssignmentId { get; set; }
    public static string TeacherName { get; set; }
    public static int TeacherId { get; set; }
    public static string CustomLevelName { get; set; }
    public static string AssignmentName { get; set; }
    public static int MonsterId { get; set; }
    public static int TimeLimit { get; set; }
    public static bool GoogleLoggedIn { get; set; }
    public static bool FbLoggedIn { get; set; }
    public static List<UserCreatedQuestion> QuestionList { get; set; }

    public static float bgmVol { get; set; }
    public static float battleBgmVol { get; set; }
    public static float sfxVol { get; set; }

    public static int FirstLoggedIn{get;set;}

    /// <summary>
    /// Global connection string
    /// </summary>
    public static MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
    {
        Server = "35.198.238.34",
        UserID = "root",
        Password = "MpiPkr9y04xmg11h",
        Database = "AdventureLearn",
        SslMode = MySqlSslMode.None,
    };

    /// <summary>
    /// Calculate Gameplay Score
    /// <param name="int timeRemaining"></param>
    /// <param name="int timeLimit"></param>
    /// </summary>
    public static int CalculateScore(int timeRemaining, int timeLimit)
    {
        double levelScore = (Convert.ToDouble(timeRemaining) / Convert.ToDouble(timeLimit)) * 100;
        return Convert.ToInt32(levelScore);
    }
    public static int GetFirstLoggedIn()
    {
        return FirstLoggedIn;
    }
    public static void SetFirstLoggedIn(int status)
    {
        FirstLoggedIn = status;
    }
    /// <summary>
    /// Store StudentId
    /// <param name="int id"></param>
    /// </summary>
    public static void SetStudentId(int id)
    {
        StudentId = id;
    }

    /// <summary>
    /// Store Student Name
    /// <param name="string name"></param>
    /// </summary>
    public static void SetStudentName(string name)
    {
        StudentName = name;
    }
    public static string GetStudentName()
    {
        return StudentName;
    }
    /// <summary>
    /// Indicate that the user has logged in using Google Account
    /// </summary>
    public static void SetGoogleLoggedIn()
    {
        GoogleLoggedIn = true;
        FbLoggedIn = false;
    }

    /// <summary>
    /// Indicate that the user has logged in using Facebook Account
    /// </summary>
    public static void SetFbLoggedIn()
    {
        FbLoggedIn = true;
        GoogleLoggedIn = false;
    }

    /// <summary>
    /// Load the set of PNGs into SpriteFrames needed for the AnimatedSprite
    /// <param name="string spritePath"></param>
    /// <param name="AnimatedSprite animatedSprite"></param>
    /// <param name="List<string> animationList"></param>
    /// </summary>
    public static void LoadSprite(string spritePath, AnimatedSprite animatedSprite, List<string> animationList)
    {
        SpriteFrames spriteFrames = new SpriteFrames();
        foreach (string animation in animationList)
        {
            var dir = new Directory();
            dir.Open(spritePath + animation);

            dir.ListDirBegin();
            var fileName = dir.GetNext();
            string strFileExtention = System.IO.Path.GetExtension(fileName);
            spriteFrames.AddAnimation(animation);
            int count = 0;

            while (!String.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Replace(strFileExtention, "");
                var sprite = ResourceLoader.Load(spritePath + animation + "/" + fileName) as Texture;
                spriteFrames.AddFrame(animation, sprite);
                fileName = dir.GetNext();
                count++;
            }
            animatedSprite.Frames = spriteFrames;
            animatedSprite.SpeedScale = 7;
        }
       
    }
}
