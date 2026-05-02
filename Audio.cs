using System;
using Godot;

namespace NeonArenaCsharp;

public partial class Audio : Node {
    public static Audio Instance { get; private set; }

    public const string BUS_MASTER = "Master";
    public const string BUS_MUSIC = "Music";
    public const string BUS_SFX = "Sfx";
    public const string BUS_UI = "Ui";

    public override void _Ready() {
        Instance = this;
    }

    public void Play(AudioStreamWav sound, string busName = BUS_MASTER, Action<AudioStreamPlayer2D> configure = null) {
        var player = new AudioStreamPlayer2D {
            Stream = sound,
            Bus = busName
        };
        configure?.Invoke(player);
        player.Finished += player.QueueFree;
        AddChild(player);
        player.Play();
    }
}
