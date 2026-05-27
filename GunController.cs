using Godot;

[GlobalClass]
public partial class GunController : Node2D {
    Gun gun;

    public override void _Process(double delta) {
        gun?.Tick((float)delta);
    }

    public void Equip(GunData data) {
        if (gun != null) {
            gun.ReloadEnded -= OnReloadEnded;
        }

        gun = new Gun(data);
        gun.ReloadEnded += OnReloadEnded;
    }

    public void DoShot() {
        if (gun != null && gun.TryDoShot()) {
            SpawnSound(GunSound.Shot);
            SpawnBullet();
        }
    }

    public void StartReload() {
        if (gun != null && gun.TryStartReload()) {
            SpawnSound(GunSound.ReloadStart);
        }
    }

    private void OnReloadEnded() {
        SpawnSound(GunSound.ReloadEnd);
    }

    private void SpawnBullet() {
        var data = gun.Data;
        if (!data.TryCreateBullet(out var bullet)) {
            return;
        }

        bullet.GlobalPosition = GlobalPosition;
        bullet.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition());
        bullet.Source = this;

        bullet.Lifetime = data.BulletLifetime;
        bullet.Speed = data.BulletSpeed;
        bullet.Damage = data.Damage;

        GetTree().CurrentScene.AddChild(bullet);
    }

    private void SpawnSound(GunSound type) {
        var data = gun.Data;
        if (data.TryGetSound(type, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }
}
