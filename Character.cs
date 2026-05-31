using System;
using System.Collections.Generic;
using Godot;

[GlobalClass]
public abstract partial class Character : CharacterBody2D, IDamageable {
    public event Action Died;

    readonly Dictionary<CharacterStat, Stat> stats = [];
    Health health;
    CharacterData data;

    [Export] public float KnockbackFriction = 100f;
    public Vector2 Knockback { get; private set; } = Vector2.Zero;

    public void Setup(CharacterData data) {
        this.data = data;
        stats[CharacterStat.Speed] = new Stat(data.Speed);
        stats[CharacterStat.MaxHp] = new Stat(data.MaxHp);

        health = new Health(stats[CharacterStat.MaxHp]);
        health.Died += Die;
    }

    public float Get(CharacterStat stat) {
        if (stats.TryGetValue(stat, out var statInstance))
            return statInstance.Value;
        return 0f;
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (data.HitSFX != null) {
            Audio.Instance.Play(data.HitSFX, Audio.BUS_SFX);
        }
    }

    public void Die() {
        Died?.Invoke();
        QueueFree();
    }

    public void ApplyKnockback(Vector2 direction, float force) {
        Knockback = direction.Normalized() * force;
    }

    protected void UpdateKnockback(float delta) {
        Knockback = Knockback.MoveToward(
            Vector2.Zero,
            delta * KnockbackFriction
        );
    }
}
