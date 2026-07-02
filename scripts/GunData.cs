using Godot;

[GlobalClass]
public partial class GunData : ItemData {
    [Export] public float Damage = 1f;
    [Export] public float FireRate = 1f / 3; // N выстрелов за секунду
    [Export] public float ReloadTime = 1f;
    [Export] public int MaxAmmo = 20;
    [Export] public float BulletSpeed = 300f;
    [Export] public float BulletLifetime = 3f;

    [Export] public float SpreadMin = 0.5f;
    [Export] public float SpreadMax = 5f;
    [Export] public float SpreadPerShot = 0.8f;
    [Export] public float SpreadRecoveryRate = 2f; // не за кадр! за секунду.

    [Export] public PackedScene BulletScene;
    [Export] public AudioStream ShotSFX;
    [Export] public AudioStream ReloadStartSFX;
    [Export] public AudioStream ReloadEndSFX;
}
