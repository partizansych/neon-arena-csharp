using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
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
        PlaySound(Data.ShotSound);
        SpawnBullet();
    }

    public bool CanReload() {
        return !isReloading && ammo < Data.MaxAmmo;
    }

    public async void Reload() {
        if (!CanReload()) return;
        isReloading = true;
        PlaySound(Data.ReloadStartSound);
        await ToSignal(GetTree().CreateTimer(Data.ReloadTime), "timeout");
        isReloading = false;
        ammo = Data.MaxAmmo;
        PlaySound(Data.ReloadEndSound);
    }

    private void SpawnBullet() {
        var bullet = Data.BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = GlobalPosition;
        bullet.Direction = ShootDirection;
        GetTree().CurrentScene.AddChild(bullet);
    }

    private void PlaySound(AudioStreamWav sound) {
        if (sound == null) return;
        Audio.Instance.Play(sound, Audio.BUS_SFX);
    }
}
