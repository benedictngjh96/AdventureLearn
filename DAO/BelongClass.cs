using Godot;
using System;
using System.Collections.Generic;
/// <summary>
/// DAO Object for BelongClass
/// </summary>
public class BelongClass : Node
{
    public int ClassId { get; set; }
    public List<Student> Student { get; set;}
}
