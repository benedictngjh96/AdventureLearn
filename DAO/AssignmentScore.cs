using Godot;
using System;
/// <summary>
/// DAO Object for AssignmentScore
/// </summary>
public class AssignmentScore : Node
{
    public Student Student { get; set; }
    public int Score {get;set;}
    public PublishedAssignment PublishedAssignment{get;set;}
}
