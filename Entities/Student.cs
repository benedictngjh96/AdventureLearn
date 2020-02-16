using Godot;
using System;

public class Student : Node
{
    public Student(string studentId, string studentName, string inGameName, int charId)
    {
        this.StudentId = studentId;
        this.StudentName = studentName;
        this.InGameName = inGameName;
        this.CharId = charId;
    }
    public string StudentId { get; set; }
    public string StudentName { get; set; }
    public string InGameName { get; set; }
    public int CharId { get; set; }
}
