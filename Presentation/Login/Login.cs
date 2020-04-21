using Godot;
using System;

public class Login : Node2D
{
    Godot.Object googleScript;
    Godot.Object fbScript;

    string email = "";
    string googleId = "";
    string facebookId = "";
    StudentBL studentBL;
    TextureButton fbBtn;
    TextureButton googleBtn;
    public override void _Ready()
    {
        GDScript fb = (GDScript)GD.Load("res://API/Facebook.gd");
        fbScript = (Godot.Object)fb.New();
        GDScript google = (GDScript)GD.Load("res://API/Google.gd");
        googleScript = (Godot.Object)google.New();
        studentBL = new StudentBL();
        fbBtn = GetNode<TextureButton>("FbLogin");
        googleBtn = GetNode<TextureButton>("GoogleLogin");
        
        fbScript.Connect("info", this, nameof(InsertFb));
        googleScript.Connect("info2", this, nameof(InsertGoogle));

    }
    private void InsertFb()
    {
        string email = fbScript.Get("email").ToString();
        string fbId = fbScript.Get("id").ToString();
        string fbName = fbScript.Get("fb_name").ToString();
        bool exist = studentBL.CheckFacebookExist(fbId);

        if (exist)
        {
            Global.StudentId = studentBL.GetFacebookStudentId(fbId);
            Global.StudentName = fbName;
            Global.FbLoggedIn = true;

            if (studentBL.CheckFacebookCharExist(fbId))
                GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
            else
                GetTree().ChangeScene("res://Presentation/CharSelect/CharSelect.tscn");
        }
        else
        {
            studentBL.InsertFacebookStudent(fbName, email, fbId);
            Global.StudentId = studentBL.GetFacebookStudentId(fbId);
            Global.StudentName = fbName;
            Global.FbLoggedIn = true;
            GetTree().ChangeScene("res://Presentation/CharSelect/CharSelect.tscn");
        }
    }
    private void InsertGoogle()
    {
        string email = googleScript.Get("email").ToString();
        string googleId = googleScript.Get("id").ToString();
        string googleName = googleScript.Get("google_name").ToString();
        bool exist = studentBL.CheckGoogleExist(googleId);

        if(exist)
        {
            GD.Print("gdsgsd");
            Global.StudentId = studentBL.GetGoogleStudentId(googleId);
            Global.StudentName = googleName;
            Global.GoogleLoggedIn = true;

            if (studentBL.CheckGoogleExist(googleId))
                GetTree().ChangeScene("res://Presentation/MainMenu/MainMenu.tscn");
            else
                GetTree().ChangeScene("res://Presentation/CharSelect/CharSelect.tscn");
        }
        else
        {
            GD.Print("dqweeq");
            studentBL.InsertGoogleStudent(googleName, email, googleId);
            Global.StudentId = studentBL.GetGoogleStudentId(googleId);
            Global.StudentName = googleName;
            Global.GoogleLoggedIn = true;
            GetTree().ChangeScene("res://Presentation/CharSelect/CharSelect.tscn");
        }
    }

    private void _on_GoogleLogin_pressed()
    {
        googleBtn.Disabled = true;
        googleScript.Call("google_connect");
        googleScript.Call("gconnect");
    }

    private void _on_FbLogin_pressed()
    {
        fbScript.Call("facebook_connect");
        fbScript.Call("login");
    }

}
