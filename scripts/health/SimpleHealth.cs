using System;
using Godot;

[GlobalClass]
public partial class SimpleHealth : Node {
    public event Action Died;
    public event Action<float, float> CurrentChanged;

    [Export] public float MaxHp { get; private set; } = 100f;
    public float Current { get; private set; }

    public override void _Ready() {
        Current = MaxHp;
    }

    public void Die() {
        Died?.Invoke();
    }

    public void Reduce(float amount) {
        if (amount <= 0f) return;
        if (Current <= 0f) return;
        var old = Current;
        Current = Mathf.Max(Current - amount, 0f);
        CurrentChanged?.Invoke(old, Current);
        if (Current <= 0f) Die();
    }

    public void Restore(float amount) {
        if (amount <= 0f) return;
        if (Current >= MaxHp) return;
        var old = Current;
        Current = Mathf.Min(Current + amount, MaxHp);
        CurrentChanged?.Invoke(old, Current);
    }
}
