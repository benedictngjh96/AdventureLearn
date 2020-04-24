using Godot;
using System;
using System.Collections.Generic;
/// <summary>
/// DAO Object for CustomLevel
/// </summary>
public class CustomLevel : Node
{
    public int CustomLevelId { get; set; }
    public Student Student { get; set; }
    public string CustomLevelName { get; set; }
    public Monster Monster { get; set; }
    public int TimeLimit { get; set; }
    public List<Question> Question { get; set; }
}
