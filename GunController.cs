using System;
using Godot;

[GlobalClass]
public partial class GunController : Node2D {
    [Export] Timer shotTimer;
    [Export] Timer reloadTimer;

    public event Action Shot;
    public event Action ReloadStarted;
    public event Action ReloadEnded;

    GunData data;
    int ammo;
    float currentSpreadDegrees;

    public override void _Ready() {
        reloadTimer.Timeout += OnReloadEnded;
    }

    public override void _PhysicsProcess(double delta) {
        if (data == null) {
            currentSpreadDegrees = 0f;
            return;
        }

        currentSpreadDegrees -= data.SpreadRecoveryRate * (float)delta;
        currentSpreadDegrees = Mathf.Max(currentSpreadDegrees, data.SpreadMin);
    }

    public void Equip(GunData data) {
        this.data = data;
        ammo = data.MaxAmmo;
    }

    public void DoShot(Vector2 direction) {
        if (data == null) return;
        if (!reloadTimer.IsStopped()) return;
        if (!shotTimer.IsStopped()) return;
        if (ammo <= 0) return;

        ammo--;

        shotTimer.Start(data.FireRate);
        SpawnBullet(direction);
        SpawnSFX(data.ShotSFX);
        AddSpread();
        // AddKnockback(); это уже сделает сама сущность, та, которой действительно это нужно
        Shot?.Invoke();
    }

    public void StartReload() {
        if (data == null) return;
        if (!reloadTimer.IsStopped()) return;

        reloadTimer.Start(data.ReloadTime);
        SpawnSFX(data.ReloadStartSFX);
        ReloadStarted?.Invoke();
    }

    private void OnReloadEnded() {
        ammo = data.MaxAmmo;

        SpawnSFX(data.ReloadEndSFX);
        ReloadEnded?.Invoke();
    }

    private void SpawnBullet(Vector2 direction) {
        if (data.BulletScene == null) return;

        var bullet = data.BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = GlobalPosition;

        bullet.Direction = GetSpreadDirection(direction);
        bullet.Source = this;
        bullet.Damage = data.Damage;
        bullet.Lifetime = data.BulletLifetime;
        bullet.Speed = data.BulletSpeed;
        bullet.KnockbackForce = 100f; //TODO: заменить на статы

        GetTree().CurrentScene.AddChild(bullet); // TODO: заменить на сервис
    }

    private static void SpawnSFX(AudioStream sfx) {
        if (sfx != null) {
            Audio.Instance.Play(sfx, Audio.BUS_SFX);
        }
    }

    private void AddSpread() {
        currentSpreadDegrees += data.SpreadPerShot;
        currentSpreadDegrees = Mathf.Min(currentSpreadDegrees, data.SpreadMax);
    }

    private Vector2 GetSpreadDirection(Vector2 startDirection) {
        var maxAngleRad = Mathf.DegToRad(currentSpreadDegrees);

        float offset;

        do {
            offset = (float)GD.Randfn(0f, maxAngleRad);
        } while (offset > maxAngleRad || offset < -maxAngleRad);

        return startDirection.Rotated(offset);
    }
}
