using Godot;
using System;

public class MainMenu : Node2D
{

	public override void _Ready()
	{

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
	private void _on_Logout_pressed()
	{
		GDScript fb = (GDScript)GD.Load("res://API/Facebook.gd");
		GDScript google = (GDScript)GD.Load("res://API/Google.gd");

		if (Global.GoogleLoggedIn)
		{
			Godot.Object googleScript = (Godot.Object)google.New(); // This is a Godot.Object
			googleScript.Call("google_connect");
			googleScript.Call("google_disconnect");
		}
		else if (Global.FbLoggedIn)
		{
			Godot.Object fbScript = (Godot.Object)fb.New(); // This is a Godot.Object
			fbScript.Call("facebook_connect");
			fbScript.Call("logout");
		}

		GetTree().ChangeScene("res://Presentation/Login/Login.tscn");
	}

}







