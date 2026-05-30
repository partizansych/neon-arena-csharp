using System.Collections.Generic;
using Godot;

[GlobalClass]
public abstract partial class Character : CharacterBody2D, IDamageable {
    [Export] Health health;

    readonly Dictionary<CharacterStat, Stat> stats = [];
    CharacterData data;

    public void Setup(CharacterData data) {
        this.data = data;
        stats[CharacterStat.Speed] = new Stat(data.Speed);
        stats[CharacterStat.MaxHp] = new Stat(data.MaxHp);
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
}
