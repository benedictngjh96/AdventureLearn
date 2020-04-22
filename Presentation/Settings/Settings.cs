using Godot;
using System;

/// <summary>
/// Class to handle Presentation for Settings
/// </summary>
public class Settings : Node2D
{
    HSlider BgmVol;
    HSlider BattleBgmVol;
    HSlider SfxVol;

    /// <summary>
    /// Initialization
    /// </summary>
    public override void _Ready()
    {
        BgmVol = GetNode<HSlider>("Menu/Bgm/BgmVolSlider");
        BattleBgmVol = GetNode<HSlider>("Menu/BattleBgm/BattleBgmVolSlider");
        SfxVol = GetNode<HSlider>("Menu/Sfx/SfxVolSlider");
    }

    /// <summary>
    /// Updates the BGM volume whenever the BGM's volume slider changes
    /// </summary>
    /// <param name="value"></param>
    private void _on_BGMVolSlider_value_changed(float value)
    {
        DefaultSound.audioPlayer.VolumeDb = value;
        Global.BgmVol = value;
    }

    /// <summary>
    /// Updates the BattleBGM volume whenever the BattleBGM's volume slider changes
    /// </summary>
    /// <param name="value"></param>
    private void _on_BattleBgmVolSlider_value_changed(float value)
    {
        Global.BattleBgmVol = value;
    }

    /// <summary>
    /// Updates the SFX volume whenever the SFX's volume slider changes
    /// </summary>
    /// <param name="value"></param>
    private void _on_SfxVolSlider_value_changed(float value)
    {
        Global.SfxVol = value;
    }

}



