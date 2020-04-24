using Godot;
using System;
using System.Collections.Generic;
/// <summary>
/// DAO Object for PublishedAssignment
/// </summary>
public class PublishedAssignment : Node
{
    public Assignment Assignment {get;set;}
    public ClassGroup ClassGroup{get;set;}
    public DateTime DueDate{get;set;}
}
