using Godot;
using System;

public class MainMenu : Node2D
{

    public override void _Ready()
    {
        /*GDScript MyGDScript = (GDScript)GD.Load("res://API/Toast.gd");
        Godot.Object myGDScriptNode = (Godot.Object)MyGDScript.New(); // This is a Godot.Object
        myGDScriptNode.Call("displayToast");*/
        
        Global.StudentId = 35;

        NotificationPopup.displayPopup("Welcome " + Global.StudentName + " !");
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
    
    private void _on_CreditsBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Credits/Credits.tscn");
    }
    
    private void _on_SettingsBtn_pressed()
    {
        GetTree().ChangeScene("res://Presentation/Settings/Settings.tscn");
    }
    
    private void welcomeMsg()
    {
        Control welcomeMsgNode = GetNode<Control>("WelcomeMsg");
        AnimationPlayer animation = GetNode<AnimationPlayer>("WelcomeMsg/AnimationPlayer");

        Label msg = GetNode<Label>("WelcomeMsg/Msg");
        msg.Text = "Welcome back, " + Global.StudentName;
        animation.Play("Hide");

        //welcomeMsgNode.Visible = false;

    }
}











