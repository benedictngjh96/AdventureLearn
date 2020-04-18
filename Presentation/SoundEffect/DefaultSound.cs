using Godot;
using System;

public class DefaultSound : Node2D
{
    public static AudioStreamPlayer audioPlayer;
    
    public override void _Ready()
    {
        audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        
    }
    public static void disableSound()
    {
        audioPlayer.Stop();
    }
    public static void enableSound()
    {
        audioPlayer.Playing = true;
    }
    public static void playSound(AudioStream audioStream)
    {
        audioPlayer.Stream = audioStream;
        audioPlayer.Play();
    }


}
