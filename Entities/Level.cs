using Godot;
using System;
using System.Collections.Generic;

public class Level : Node
{
    public int WorldId { get; set; }
    public int SectionId { get; set; }
    public int LevelId { get; set; }
    public int MonsterId { get; set; }
    public int TimeLimit { get; set; }
    public List<Question> Question { get; set; }
    //public List<Question> Question { get; set; }
}
