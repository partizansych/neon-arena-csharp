using Godot;

[GlobalClass]
public partial class Enemy : CharacterBody2D {
    [Signal] public delegate void DiedEventHandler();

    [Export] StatsContainer stats;
    [Export] Health health;

    public void Setup(EnemyData data) {
        stats.Setup(data.Stats);
    }

    public override void _Ready() {
        health.Died += () => EmitSignal(SignalName.Died);
    }

    public void TakeDamage(float amount) {
        health.Current -= amount;
    }
}
