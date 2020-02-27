using Godot;
using System;
using System.Collections.Generic;

public class LeaderboardScreen : Node
{
    ItemList itemList;
    OptionButton optionBtn;
    LeaderboardBL leaderboardBL;
    List<World> worldList;
    public override void _Ready()
    {
        leaderboardBL = new LeaderboardBL();
        optionBtn = GetNode<OptionButton>("OptionButton");
        itemList = GetNode<ItemList>("ItemList");

        //Default setting
        optionBtn.AddItem("All Worlds");
        DisplayWorldList();
        DisplayLeaderboard();
    }
    private void DisplayWorldList()
    {
        worldList = leaderboardBL.GetWorlds();
        foreach (World w in worldList)
            optionBtn.AddItem(w.WorldName);
    }
    private void DisplayLeaderboard()
    {
        List<Leaderboard> leaderboardList = leaderboardBL.GetLeaderboards();
        foreach (Leaderboard lb in leaderboardList)
            itemList.AddItem(lb.StudentName + "                 " + lb.TotalScore);
    }
    private void _on_OptionButton_item_selected(int id)
    {
        World world = new World();
        int worldId = 0;

        foreach(World w in worldList){
            if(w.WorldName == optionBtn.Text)
                worldId = w.WorldId;
                break;
        }
        leaderboardBL.GetWorldLeaderboard(worldId);
    }
}





