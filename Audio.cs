using Godot;

public partial class Audio : Node {
    public static Audio Instance { get; private set; }

    public override void _Ready() {
        Instance = this;
    }

    public void Play(AudioStreamWav sound) {
        var player = new AudioStreamPlayer2D {
            Stream = sound
        };
        player.Finished += player.QueueFree;
        AddChild(player);
        player.Play();
    }
}
