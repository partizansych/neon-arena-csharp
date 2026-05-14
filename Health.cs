using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class Health : Node2D {
    [Signal] public delegate void CurrentChangedEventHandler(float oldValue, float newValue);
    [Signal] public delegate void DiedEventHandler();

    float maxHp = 100f; // TODO: переделать в статы
    float current;

    public override void _Ready() {
        current = maxHp;

        if (!IsInGroup("healths"))
            AddToGroup("healths");
    }

    public void Reduce(float amount) {
        if (amount <= 0f) return;
        if (current <= 0f) return;
        var old = current;
        current = Mathf.Max(current - amount, 0f);
        EmitSignal(SignalName.CurrentChanged, old, current);
        if (current <= 0f) Die();
    }

    public void Restore(float amount) {
        if (amount <= 0f) return;
        if (current >= maxHp) return;
        var old = current;
        current = Mathf.Min(current + amount, maxHp);
        EmitSignal(SignalName.CurrentChanged, old, current);
    }

    public void Die() {
        EmitSignal(SignalName.Died);
    }
}
