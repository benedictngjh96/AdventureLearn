using Godot;
using System;

/// <summary>
/// Class to handle Presentation for Section
/// </summary>
public class MainMenu : Node2D
{
    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        GDScript MyGDScript = (GDScript)GD.Load("res://API/Toast.gd");
        Godot.Object myGDScriptNode = (Godot.Object)MyGDScript.New(); // This is a Godot.Object
        myGDScriptNode.Call("displayToast");
    }

    /// <summary>
    /// Change scene to World.tscn whenever the Campaign button is pressed
    /// </summary>
    private void _on_CampaignBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/World/World.tscn");
    }

    /// <summary>
    /// Change scene to ViewCustomLevel.tscn whenever the CustomLevel button is pressed
    /// </summary>
    private void _on_CustomLevelBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/CustomLevel/ViewCustomLevel.tscn");
    }

    /// <summary>
    /// Change scene to ViewAssignment.tscn whenever the Assignment button is pressed
    /// </summary>
    private void _on_AssignmentBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Assignment/ViewAssignment.tscn");
    }

    /// <summary>
    /// Change scene to Leaderboard.tscn whenever the Leaderboard button is pressed
    /// </summary>
    private void _on_LeaderboardBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Leaderboard/Leaderboard.tscn");
    }

    /// <summary>
    /// Change scene to UserProfile.tscn whenever the Profile button is pressed
    /// </summary>
    private void _on_UserProfileBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/UserProfile/UserProfile.tscn");
    }

    /// <summary>
    /// Handles the logic whenever the Logout button is pressed
    /// </summary>
    private void _on_LogoutBtn_pressed()
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

    /// <summary>
    /// Change scene to Settings.tscn whenever the Settings button is pressed
    /// </summary>
    private void _on_SettingsBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Settings/Settings.tscn");
    }

    /// <summary>
    /// Change scene to Credits.tscn whenever the Credits button is pressed
    /// </summary>
    private void _on_CreditsBtn2_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Credits/Credits.tscn");
    }

}











