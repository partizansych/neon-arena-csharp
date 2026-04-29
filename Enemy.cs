using Godot;
using NeonArenaCsharp;

[GlobalClass]
public partial class Enemy : CharacterBody2D, IDamageable {
    [Signal] public delegate void DiedEventHandler();

    [Export] StatsContainer stats;
    [Export] Health health;

    public override void _Ready() {
        health.Died += () => EmitSignal(SignalName.Died);
    }

    public void TakeDamage(float amount) {
        health.Current -= amount;
    }
}
