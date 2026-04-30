using Godot;
using NeonArenaCsharp;

[GlobalClass]
public partial class Enemy : CharacterBody2D, IDamageable {
    [Signal] public delegate void DiedEventHandler();

    [ExportGroup("Ассеты")]
    [Export] AudioStreamWav HitSound;
    [Export] AudioStreamWav DeathSound;

    [ExportGroup("Компоненты")]
    [Export] StatsContainer stats;
    [Export] Health health;

    public override void _Ready() {
        health.Died += OnDied;
    }

    public void TakeDamage(float amount) {
        health.Current -= amount;
        Audio.Instance.Play(HitSound);
    }

    private void OnDied() {
        Audio.Instance.Play(DeathSound);
        EmitSignal(SignalName.Died);
    }
}
