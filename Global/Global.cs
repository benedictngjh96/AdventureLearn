using System;
using Godot;
using MySql.Data.MySqlClient;

public class Global : Node
{
    public static string studentId;
    public static string charName;
    public static string studentName;

    public static MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
    {
        Server = "35.198.238.34",
        UserID = "root",
        Password = "MpiPkr9y04xmg11h",
        Database = "AdventureLearn",
        SslMode = MySqlSslMode.None,
    };

    public static void SetStudentId(string studentId)
    {
        Global.studentId = studentId;
    }
    public static void SetCharName(string charName)
    {
        Global.charName = charName;
    }
    public static void SetStudentName(string studentName)
    {
        Global.studentName = studentName;
    }

}
