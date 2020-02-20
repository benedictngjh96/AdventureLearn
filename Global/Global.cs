using System;
using Godot;
using MySql.Data.MySqlClient;

public class Global : Node
{
    //public static int studentId;
    //public static string charName;
    //public static string studentName;
    //public static int worldId;
    //public static int sectionId;
    //public static int levelId;
    //public static string levelSelected;
    public static int StudentId { get; set; }
    public static int WorldId { get; set; }
    public static int SectionId { get; set; }
    public static int LevelId { get; set; }
    public static string LevelSelected { get; set; }


    public static MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
    {
        Server = "35.198.238.34",
        UserID = "root",
        Password = "MpiPkr9y04xmg11h",
        Database = "AdventureLearn",
        SslMode = MySqlSslMode.None,
    };

    

}
