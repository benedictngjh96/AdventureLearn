using Godot;
using System;

public class Question : Node
{
    public int QuestionId { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string CorrectOption { get; set; }
    public string QuestionTitle { get; set; }
}
