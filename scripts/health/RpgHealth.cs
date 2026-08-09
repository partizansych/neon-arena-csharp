using System;
using Godot;

[GlobalClass]
public partial class RpgHealth : Node {
    public event Action Died;
    public event Action<float, float> CurrentChanged;

    [Export] private StatContainer stats;
    public float Current { get; private set; }

    public override void _Ready() {
        Current = stats.GetValue(StatType.MaxHp);
    }

    public void Reduce(float amount) {
        if (amount <= 0f) return;
        if (Current <= 0f) return;
        var old = Current;
        Current = Mathf.Max(Current - amount, 0f);
        CurrentChanged?.Invoke(old, Current);
        if (Current <= 0f) Died?.Invoke();
    }

    public void Restore(float amount) {
        if (amount <= 0f) return;

        var maxHp = stats.GetValue(StatType.MaxHp);
        if (Current < maxHp) {
            var old = Current;
            Current = Mathf.Min(Current + amount, maxHp);
            CurrentChanged?.Invoke(old, Current);
        }
    }
}
