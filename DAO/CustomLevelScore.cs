using Godot;
using System;
using System.Collections.Generic;
/// <summary>
/// DAO Object for CustomLevelScore
/// </summary>
public class CustomLevelScore : Node
{
    public Student Student { get; set; }
    public CustomLevel CustomLevel { get; set; }
    public int LevelScore { get; set; }
}
