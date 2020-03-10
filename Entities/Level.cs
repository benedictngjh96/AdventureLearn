using Godot;
using System;
using System.Collections.Generic;

public class Level : Node
{
    public int LevelId { get; set; }
    public Monster Monster { get; set; }
    public int TimeLimit { get; set; }
    public List<Question> Question { get; set; }
}
