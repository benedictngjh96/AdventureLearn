using Godot;
using System;
using System.Collections.Generic;

public class CampaignQuestion : Node
{
   public int WorldId { get; set; }
   public int SectionId { get; set; }
   public int LevelId { get; set; }
   public int Points { get; set; }
   public int MonsterId { get; set; }
   
   public Question Question { get; set; }

}
