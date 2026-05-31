using Godot;

[GlobalClass]
public partial class GunController : Node2D {
    Gun gun;

    float currentSpreadDegrees;

    public override void _Process(double delta) {
        if (gun != null) {
            gun.Tick((float)delta);

            var data = gun.Data;
            currentSpreadDegrees -= data.SpreadRecoveryRate * (float)delta;
            currentSpreadDegrees = Mathf.Max(currentSpreadDegrees, data.SpreadMin);
        }
        else {
            currentSpreadDegrees = 0f;
        }
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

            var data = gun.Data;
            currentSpreadDegrees += data.SpreadPerShot;
            currentSpreadDegrees = Mathf.Min(currentSpreadDegrees, data.SpreadMax);
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
        bullet.Direction = GetSpreadDirection();
        bullet.Source = this;
        bullet.Damage = data.Damage;
        bullet.Lifetime = data.BulletLifetime;
        bullet.Speed = data.BulletSpeed;
        bullet.KnockbackForce = 100f; //TODO: заменить на статы

        GetTree().CurrentScene.AddChild(bullet);
    }

    private void SpawnSound(GunSound type) {
        var data = gun.Data;
        if (data.TryGetSound(type, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }

    private Vector2 GetSpreadDirection() {
        var startDirection = GlobalPosition.DirectionTo(GetGlobalMousePosition());
        var maxAngleRad = Mathf.DegToRad(currentSpreadDegrees);

        float offset;

        do {
            offset = (float)GD.Randfn(0f, maxAngleRad);
        } while (offset > maxAngleRad || offset < -maxAngleRad);

        return startDirection.Rotated(offset);
    }
}
