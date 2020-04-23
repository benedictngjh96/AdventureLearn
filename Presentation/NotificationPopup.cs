using Godot;
using System;

/// <summary>
/// Class to handle Presentation for NotificationPopup
/// </summary>
public class NotificationPopup : Control
{
    public static Control node;
    public static Label msg;
    public static AnimationPlayer animations;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        msg = GetNode<Label>("msg");
        animations = GetNode<AnimationPlayer>("Animations");
        node = (Control)msg.GetParent();
    }

    /// <summary>
    /// Display the message in the parameter as popup message
    /// </summary>
    /// <param name="message"></param>
    public static void DisplayPopup(string message)
    {
        msg.Text = message;
        animations.Play("Hide");
    }

}
