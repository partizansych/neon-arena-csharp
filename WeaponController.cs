using Godot;

namespace NeonArenaCsharp;

public partial class WeaponController : Node2D {
    [Export] public WeaponData Data;

    public Vector2 ShootDirection = Vector2.Right;
    public bool isShooting;

    float timeSinceShot;
    int ammo;
    bool isReloading;

    public override void _Ready() {
        timeSinceShot = Data.FireRate;
        ammo = Data.MaxAmmo;
    }

    public override void _Process(double delta) {
        if (timeSinceShot < Data.FireRate)
            timeSinceShot += (float)delta;

        if (isShooting)
            DoShot();
    }

    public bool CanDoShot() {
        return !isReloading && timeSinceShot >= Data.FireRate && ammo > 0;
    }

    public void DoShot() {
        if (!CanDoShot()) return;
        timeSinceShot = 0f;
        ammo--;
        SpawnBullet();
    }

    public bool CanReload() {
        return !isReloading && ammo < Data.MaxAmmo;
    }

    public async void Reload() {
        if (!CanReload()) return;
        isReloading = true;
        await ToSignal(GetTree().CreateTimer(Data.ReloadTime), "timeout");
        isReloading = false;
        ammo = Data.MaxAmmo;
    }

    private void SpawnBullet() {
        var bullet = Data.BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = GlobalPosition;
        bullet.Direction = ShootDirection;
        GetTree().CurrentScene.AddChild(bullet);
    }
}
