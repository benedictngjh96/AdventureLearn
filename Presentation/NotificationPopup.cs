using Godot;
using System;

public class NotificationPopup : Control
{
    public static Control node;
    public static Label msg;
    public static AnimationPlayer animations;

    public override void _Ready()
    {
        msg = GetNode<Label>("msg");
        animations = GetNode<AnimationPlayer>("Animations");
        node = (Control)msg.GetParent();
    }

    public static void displayPopup(string message)
    {
        msg.Text = message;
        animations.Play("Hide");
    }

}
