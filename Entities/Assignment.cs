using Godot;
using System;
using System.Collections.Generic;

public class Assignment : Node
{
    public int AssignmentId { get; set; }
    public Teacher Teacher { get; set; }
    public string AssignmentName { get; set; }
    public Monster Monster { get; set; }
    public int TimeLimit { get; set; }
    public List<Question> Question { get; set; }
}
