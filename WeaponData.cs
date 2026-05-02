using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class WeaponData : Resource {
    [Export] public float Damage = 1f;
    [Export] public float FireRate = 1f / 3; // N выстрелов за секунду
    [Export] public float ReloadTime = 1f;
    [Export] public int MaxAmmo = 10;
    [Export] public PackedScene BulletScene;

    [ExportGroup("Звуки")]
    [Export] public AudioStreamWav ShotSound { get; private set; }
    [Export] public AudioStreamWav ReloadStartSound { get; private set; }
    [Export] public AudioStreamWav ReloadEndSound { get; private set; }
}
