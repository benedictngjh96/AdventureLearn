using Godot;
using System;

/// <summary>
/// Class to handle Presentation for DefaultSound
/// </summary>
public class DefaultSound : Node2D
{
    public static AudioStreamPlayer audioPlayer;
    
    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        
    }

    /// <summary>
    /// Disable the sound
    /// </summary>
    public static void disableSound()
    {
        audioPlayer.Stop();
    }

    /// <summary>
    /// Enable the sound
    /// </summary>
    public static void enableSound()
    {
        audioPlayer.Playing = true;
    }

    /// <summary>
    /// Play the sound
    /// </summary>
    /// <param name="audioStream"></param>
    public static void playSound(AudioStream audioStream)
    {
        audioPlayer.Stream = audioStream;
        audioPlayer.Play();
    }


}
