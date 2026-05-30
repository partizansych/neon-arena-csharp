using System;
using Godot;

public class Health {
    public event Action Died;
    public event Action<float, float> CurrentChanged;

    readonly Stat maxHpStat;

    public float MaxHp => maxHpStat.Value;
    public float Current { get; private set; }

    public Health(Stat maxHpStat) {
        this.maxHpStat = maxHpStat;
        Current = maxHpStat.Value;
        maxHpStat.Changed += OnMaxHpChanged;
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

    public void Die() {
        Died?.Invoke();
    }

    private void OnMaxHpChanged(float oldValue, float newValue) {
        if (newValue <= 0f) {
            Die();
        }
        else if (newValue > oldValue) {
            Restore(newValue - oldValue);
        }
        else if (newValue < oldValue) {
            Reduce(oldValue - newValue);
        }
    }
}
