using System;
using Godot;
using MySql.Data.MySqlClient;

public class Global : Node
{
    /// <summary>
    /// Global variables
    /// </summary>
    public static string GoogleId{ get; set;}
    public static string StudentName{ get; set;}
    public static string StudentId { get; set; }
    public static int WorldId { get; set; }
    public static int SectionId { get; set; }
    public static int LevelId { get; set; }
    public static int CustomLevelId { get; set; }
    public static int AssignmentId { get; set; }
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
    public static int CalculateScore(int timeRemaining , int timeLimit)
    {
        double levelScore = Convert.ToDouble(timeRemaining) / Convert.ToDouble(timeLimit) * 100;
        return Convert.ToInt32(levelScore);
    }
    public static void SetGoogleId(string id){
        GoogleId = id;
    }
    public static void SetStudentName(string name){
        StudentName = name;
    }
    

    

}
