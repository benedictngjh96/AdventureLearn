using Godot;
using System;
/// <summary>
/// DAO Object for StudentScore
/// </summary>
public class StudentScore : Node
{
    public string StudentId { get; set; }
    public int WorldId { get; set; }
    public int SectionId { get; set; }
    public int LevelId { get; set; }
    public int LevelScore { get; set; }

}
