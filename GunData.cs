using Godot;

namespace NeonArenaCsharp;

public enum GunSound {
    Shot,
    ReloadStart,
    ReloadEnd
}

public enum GunStat {
    Damage,
    FireRate,
    MaxAmmo,
    ReloadTime,
    BulletSpeed,
    BulletLifetime
}

[GlobalClass]
public partial class GunData : Resource {
    [Export] public string Id;
    [Export] public Texture2D Icon;

    [ExportGroup("Базовые значения аттрибутов")]
    [Export] public float Damage = 1f;
    [Export] public float FireRate = 1f / 3; // N выстрелов за секунду
    [Export] public float ReloadTime = 1f;
    [Export] public int MaxAmmo = 20;
    [Export] public float BulletSpeed = 300f;
    [Export] public float BulletLifetime = 3f;

    [ExportGroup("")]
    [Export] PackedScene bulletScene;
    [Export] Godot.Collections.Dictionary<GunSound, AudioStreamWav> sounds;

    public bool TryCreateBullet(out Bullet bullet) {
        bullet = null;
        if (bulletScene != null) {
            bullet = bulletScene.Instantiate<Bullet>();
            return true;
        }
        return false;
    }

    public bool TryGetSound(GunSound type, out AudioStreamWav sound) {
        return sounds.TryGetValue(type, out sound);
    }
}
