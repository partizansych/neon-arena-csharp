using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class GunStatSheet : Node {
    readonly Dictionary<GunStat, Stat> stats = [];

    public void Setup(GunData data) {
        stats[GunStat.Damage] = new Stat(data.Damage);
        stats[GunStat.FireRate] = new Stat(data.FireRate);
        stats[GunStat.MaxAmmo] = new Stat(data.MaxAmmo);
        stats[GunStat.ReloadTime] = new Stat(data.ReloadTime);
        stats[GunStat.BulletSpeed] = new Stat(data.BulletSpeed);
        stats[GunStat.BulletLifetime] = new Stat(data.BulletLifetime);
    }

    public float Get(GunStat type) {
        if (stats.TryGetValue(type, out var stat))
            return stat.Value;
        return 0f;
    }
}
