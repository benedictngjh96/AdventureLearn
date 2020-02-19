using Godot;
using System;
using System.Collections.Generic;

public class Student : Node
{
    public Student(string studentName, int charId, string studentEmail,string studentUsername, string studentPassword)
    {
        this.StudentName = studentName;
        this.CharId = charId;
        this.StudentEmail = StudentEmail;
        this.StudentUserName = StudentUserName;
        this.StudentPassword = StudentPassword;
    }
    public Student()
    {

    }
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public int CharId { get; set; }

    public string StudentEmail { get; set; }
    public string StudentUserName { get; set; }
    public string StudentPassword { get; set; }
    public List<StudentScore> StudentScore { get; set; }
}
