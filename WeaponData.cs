using Godot;

namespace NeonArenaCsharp;

public enum WeaponSound {
    Shot,
    ReloadStart,
    ReloadEnd
}

[GlobalClass]
public partial class WeaponData : Resource {
    [Export] public PackedScene BulletScene;
    [Export] public Godot.Collections.Dictionary<WeaponSound, AudioStreamWav> Sounds;

    [ExportGroup("Базовые значения аттрибутов")]
    [Export] public float Damage = 1f;
    [Export] public float FireRate = 1f / 3; // N выстрелов за секунду
    [Export] public float ReloadTime = 1f;
    [Export] public int MaxAmmo = 20;
    [Export] public float BulletSpeed = 300f;
    [Export] public float BulletLifetime = 3f;
}
