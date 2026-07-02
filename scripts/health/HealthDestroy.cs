using Godot;

[GlobalClass]
public partial class HealthDestroy : Node {
    [Export] Node target;
    [Export] SimpleHealth health;

    public override void _Ready() {
        if (target == null) {
            GD.PushError("Ссылка на 'target' не установлена.");
            return;
        }

        if (health == null) {
            GD.PushError("Ссылка на 'SimpleHealth' не установлена.");
            return;
        }

        health.Died += OnDied;
    }

    private void OnDied() {
        if (target == null) return;
        target.QueueFree();
    }
}
