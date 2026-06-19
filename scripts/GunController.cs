using System;
using Godot;

[GlobalClass]
public partial class GunController : Node2D {
    [Export] Timer shotTimer;
    [Export] Timer reloadTimer;
    [Export] SpreadComponent spread;

    public event Action Shot;
    public event Action ReloadStarted;
    public event Action ReloadEnded;

    GunData data;
    int ammo;

    public override void _Ready() {
        reloadTimer.Timeout += OnReloadEnded;
    }

    public override void _Process(double delta) {
        // Чтобы если что видеть debug линию нормально
        spread.LookAt(GetGlobalMousePosition());
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
        spread.Apply(3f);
        SpawnBullet(direction);
        SpawnSFX(data.ShotSFX);
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

        bullet.Direction = spread.ModifyDirection(direction);
        bullet.Source = this;
        bullet.Damage = data.Damage;
        bullet.Lifetime = data.BulletLifetime;
        bullet.Speed = data.BulletSpeed;
        bullet.KnockbackForce = 300f; //TODO: заменить на статы

        GetTree().CurrentScene.AddChild(bullet); // TODO: заменить на сервис
    }

    private static void SpawnSFX(AudioStream sfx) {
        if (sfx != null) {
            Audio.Instance.PlayJuicySFX(sfx);
        }
    }
}
