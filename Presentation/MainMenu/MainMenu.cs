using Godot;
using System;

public class MainMenu : Node2D
{
	Node2D parentNode;
	Control loadingScreenNode;

	public static int welcomeOnce = 0;
	Control welcomeMsgNode;
	AnimationPlayer animations;
	Label welcomeMsgText;

	public override void _Ready()
	{
		//Testing
		Global.StudentName = "Yuen Kim Hwee";
		//Testing

		if (welcomeOnce == 0)
		{
			welcomeMsgNode = GetNode<Control>("WelcomeMsg");
			animations = GetNode<AnimationPlayer>("WelcomeMsg/Animations");
			welcomeMsgText = GetNode<Label>("WelcomeMsg/Msg");

			
			welcomeMsgText.Text = "Welcome back, " + Global.StudentName;
			animations.Play("Hide");

			welcomeOnce = 1;
		}
	}

	private void _on_CampaignBtn_pressed()
	{
		GetTree().ChangeScene("res://Presentation/World/World.tscn");
	}

	private void _on_CustomLevelBtn_pressed()
	{
		GetTree().ChangeScene("res://Presentation/CustomLevel/ViewCustomLevel.tscn");
	}

	private void _on_AssignmentBtn_pressed()
	{
		GetTree().ChangeScene("res://Presentation/Assignment/ViewAssignment.tscn");
	}

	private void _on_LeaderboardBtn_pressed()
	{
		GetTree().ChangeScene("res://Presentation/Leaderboard/Leaderboard.tscn");
	}

	private void _on_UserProfileBtn_pressed()
	{
		GetTree().ChangeScene("res://Presentation/UserProfile/UserProfile.tscn");
	}

	private void _on_LogoutBtn_pressed()
	{
		welcomeOnce = 0;

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

	private void _on_SettingsBtn_pressed()
	{
		GetTree().ChangeScene("res://Presentation/Settings/Settings.tscn");
	}

	private void _on_ShowLoad_pressed()
	{
		LoadingScreen.show();
	}


	private void _on_HideLoad_pressed()
	{
		LoadingScreen.hide();
	}
 
}









