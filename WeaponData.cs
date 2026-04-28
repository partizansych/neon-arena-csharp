using Godot;

public partial class WeaponData : Resource {
    [Export] public float FireRate = 1f / 3; // N выстрелов за секунду
    [Export] public float ReloadTime = 1f;
    [Export] public int MaxAmmo = 10;
    [Export] public PackedScene BulletScene;
}
