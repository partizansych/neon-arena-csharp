using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class Character : CharacterBody2D, IDamageable {
    [Signal] public delegate void DiedEventHandler();

    [ExportGroup("Компоненты")]
    [Export] public StatsContainer Stats;
    [Export] public Health Health;

    [ExportGroup("Звуки")]
    [Export] AudioStreamWav HitSound;
    [Export] AudioStreamWav DeathSound;

    public override void _Ready() {
        Health.Died += OnDied;
    }

    public void TakeDamage(float amount) {
        Health.Reduce(amount);
        Audio.Instance.Play(HitSound, Audio.BUS_SFX);
    }

    private void OnDied() {
        Audio.Instance.Play(DeathSound, Audio.BUS_SFX);
        EmitSignal(SignalName.Died);
        QueueFree();
    }
}
