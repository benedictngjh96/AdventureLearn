using Godot;
using System;
using System.Collections.Generic;

public class LeaderboardScreen : Node
{
    ItemList itemList;
    OptionButton optionBtn;
    LeaderboardBL leaderboardBL;
    public override void _Ready()
    {
        leaderboardBL = new LeaderboardBL();
        
        optionBtn = GetNode<OptionButton>("OptionButton");
        itemList = GetNode<ItemList>("ItemList");
  
        DisplayWorldList();
        DisplayLeaderboard();
    }
    private void DisplayWorldList()
    {
        List<World> worldList = leaderboardBL.GetWorlds();
        foreach (World w in worldList)
            optionBtn.AddItem(w.WorldName);
    }
    private void DisplayLeaderboard()
    {
        List<Leaderboard> leaderboardList = leaderboardBL.GetLeaderboards();
        foreach (Leaderboard lb in leaderboardList)
            itemList.AddItem(lb.StudentName + "                 " + lb.TotalScore);
    }
    private void _on_OptionButton_item_selected()
    {
        GD.Print("CHANGE");
    }
}
