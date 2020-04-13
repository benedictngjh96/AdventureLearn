using Godot;
using System;
using System.Collections.Generic;

public class LeaderboardScreen : Node
{
    OptionButton optionBtn;
    LeaderboardBL leaderboardBL;
    List<World> worldList;

    VBoxContainer vbox;
    DynamicFont dFont;
    GridContainer gridContainer;
    List<Leaderboard> leaderboardList;
    public override void _Ready()
    {

        vbox = GetNode<VBoxContainer>("VBoxContainer");
        gridContainer = GetNode<GridContainer>("VBoxContainer/GridContainer");

        leaderboardBL = new LeaderboardBL();
        optionBtn = GetNode<OptionButton>("OptionButton");

        //Default setting
        optionBtn.AddItem("All Worlds");
        DisplayWorldList();
        leaderboardList = leaderboardBL.GetLeaderboards();
        AddHeader();
        DisplayLeaderboard(leaderboardList);
    }
    private void AddHeader()
    {
        dFont = new DynamicFont();
        dFont.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont.Size = 26;

        Label rank = new Label();
        rank.AddFontOverride("font", dFont);
        rank.Text = "Rank                               ";
        gridContainer.AddChild(rank);

        Label lbl = new Label();
        lbl.AddFontOverride("font", dFont);
        lbl.Text = "Name                                    ";
        gridContainer.AddChild(lbl);

        Label lbl2 = new Label();
        lbl2.AddFontOverride("font", dFont);
        lbl2.Text = "Score";
        gridContainer.AddChild(lbl2);


    }
    private void DisplayWorldList()
    {
        worldList = leaderboardBL.GetWorlds();
        foreach (World w in worldList)
            optionBtn.AddItem(w.WorldName);
    }
    private void ClearGrid()
    {
        foreach (Node child in gridContainer.GetChildren())
            gridContainer.RemoveChild(child);
        AddHeader();
    }
    private void DisplayLeaderboard(List<Leaderboard> leaderboardList)
    {
        ClearGrid();

        DynamicFont dFont2 = new DynamicFont();
        dFont2.FontData = ResourceLoader.Load("res://Fonts/Candy Beans.otf") as DynamicFontData;
        dFont2.Size = 15;
        int i = 1;
        foreach (Leaderboard lb in leaderboardList)
        {
            Label rank = new Label();
            rank.AddFontOverride("font", dFont2);
            rank.Text = i.ToString();
            gridContainer.AddChild(rank);
            //Student Name
            Label lbl = new Label();
            lbl.AddFontOverride("font", dFont2);
            lbl.Text = lb.StudentName;
            gridContainer.AddChild(lbl);
            /*
            //Sprite
            Sprite charSprite = new Sprite();
            var texture = ResourceLoader.Load(String.Format("res://CharSprites/{0}/Head/head.png", lb.CharName)) as Texture;
            charSprite.Texture = texture;
            charSprite.Scale= new Vector2(0.15f, 0.15f);
            gridContainer.AddChild(charSprite);
            */
            //Total Score
            Label lbl2 = new Label();
            lbl2.AddFontOverride("font", dFont2);
            lbl2.Text = lb.TotalScore.ToString();

            gridContainer.AddChild(lbl2);
            //vbox.AddChild(lbl);
            i++;
        }

        // itemList.AddItem(lb.StudentName + "                 " + lb.TotalScore);
    }
    private void _on_OptionButton_item_selected(int id)
    {
        World world = new World();

        if (id == 0)
            DisplayLeaderboard(leaderboardBL.GetLeaderboards());
        else
            DisplayLeaderboard(leaderboardBL.GetWorldLeaderboard(id));
    }
}





