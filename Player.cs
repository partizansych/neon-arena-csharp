using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D, IDamageable {
    [Export] Health health;

    PlayerData data;

    readonly Dictionary<PlayerStat, Stat> stats = [];

    public override void _Process(double delta) {
        // if (gun != null && Input.IsActionPressed("attack")) {
        //     var mousePos = GetGlobalMousePosition();
        //     var direction = GlobalPosition.DirectionTo(mousePos);
        //     gun.DoShot(direction);
        // }
    }

    public void Setup(PlayerData data) {
        this.data = data;
        stats[PlayerStat.Speed] = new Stat(data.Speed);
        stats[PlayerStat.MaxHp] = new Stat(data.MaxHp);
    }

    public float Get(PlayerStat stat) {
        if (stats.TryGetValue(stat, out var statInstance))
            return statInstance.Value;
        return 0f;
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (data.TryGetSound(PlayerSound.Hit, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }
}
