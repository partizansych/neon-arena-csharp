using Godot;

[GlobalClass]
public partial class Health : Node2D {
    [Signal] public delegate void CurrentChangedEventHandler(float oldValue, float newValue);
    [Signal] public delegate void DiedEventHandler();

    [Export] StatsContainer stats;
    float current;

    public float Current {
        get => current;
        set {
            if (current == value) return;
            float old = current;
            current = value;
            EmitSignal(SignalName.CurrentChanged, old, current);
            if (current == 0f) Die();
        }
    }

    public float MaxHp {
        get => stats.MaxHp.Value;
    }

    public override void _Ready() {
        current = MaxHp;

        if (!IsInGroup("healths"))
            AddToGroup("healths");
    }

    public void Die() {
        Owner.QueueFree();
        EmitSignal(SignalName.Died);
    }
}
