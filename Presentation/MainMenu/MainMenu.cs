using Godot;
using System;

public class MainMenu : Node2D
{

    public override void _Ready()
    {
        Godot.GD.Print(Global.StudentId);

    }
    private void _on_WorldBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/World/World.tscn");
    }
    private void _on_ChallengeBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/CustomLevel/ViewCustomLevel.tscn");
    }
    private void _on_LeaderboardBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Leaderboard/Leaderboard.tscn");
    }
    private void _on_UserProfileBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/UserProfile/UserProfile.tscn");
    }

    private void _on_AssignmentBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Assignment/ViewAssignment.tscn");
    }

    private void _on_LvlBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/CreateLevel/CreateLevelInit.tscn");
    }

}







