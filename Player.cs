using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D, IDamageable {
    [Export] Health health;
    [Export] GunController gunController;

    PlayerData playerData;

    readonly Dictionary<PlayerStat, Stat> stats = [];

    public override void _PhysicsProcess(double delta) {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * Get(PlayerStat.Speed);
        MoveAndSlide();

        if (Input.IsActionPressed("attack")) {
            gunController.DoShot();
        }
        if (Input.IsKeyPressed(Key.R)) {
            gunController.StartReload();
        }
    }

    public void Setup(PlayerData data) {
        playerData = data;
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
        if (playerData.TryGetSound(PlayerSound.Hit, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }

    public void EquipGun(GunData data) {
        gunController.Equip(data);
    }
}
