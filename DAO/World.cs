using Godot;
using System;
using System.Collections.Generic;
/// <summary>
/// DAO Object for World
/// </summary>
public class World : Node
{
    public int WorldId { get; set; }
    public string WorldName { get; set; }
    public List<Section> Section { get; set; }
}
