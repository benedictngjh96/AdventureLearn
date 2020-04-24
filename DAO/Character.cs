using Godot;
using System;

/// <summary>
/// DAO Object for Character
/// </summary>
public class Character : Node
{
    public int CharId { get; set; }
    public string CharName { get; set; }
    public string CharSkill { get; set; }
    public string SkillDescription {get;set;}
}
