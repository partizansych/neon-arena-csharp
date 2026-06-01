using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D {
    [Export] SimpleHealth health;
    [Export] InputMovement input;
    [Export] KnockbackComponent knockback;
    [Export] public AudioStream HitSFX;
    [Export] public AudioStream DeathSFX;

    [ExportGroup("Debug")]
    [Export] public bool Debug;
    [Export] public Color DebugColor = Colors.Red;
    [Export] public float DebugCircleRadius = 2.5f;
    [Export] public float DebugMaxLineLength = 100f;
    [Export] public int DebugFontSize = 14;
    [Export] public Vector2 DebugFontOffset = new(8, -5);

    public override void _Ready() {
        health.Died += OnDied;
    }

    public override void _PhysicsProcess(double delta) {
        Velocity = input.Velocity + knockback.Velocity;
        MoveAndSlide();

        if (Debug)
            QueueRedraw();

        if (gun != null) {
            gun.Tick((float)delta);

            var data = gun.Data;
            currentSpreadDegrees -= data.SpreadRecoveryRate * (float)delta;
            currentSpreadDegrees = Mathf.Max(currentSpreadDegrees, data.SpreadMin);
        }
        else {
            currentSpreadDegrees = 0f;
        }

        if (Input.IsActionPressed("attack")) {
            DoShot();
        }
        if (Input.IsKeyPressed(Key.R)) {
            StartReload();
        }
    }

    public override void _Draw() {
        if (!Debug) return;
        DrawSetTransformMatrix(GlobalTransform.AffineInverse());
        var scaledVector = Velocity * (DebugMaxLineLength / 400f);
        var start = GlobalPosition;
        var end = start + scaledVector;
        DrawLine(start, end, DebugColor);
        DrawCircle(end, DebugCircleRadius, DebugColor);
        var font = ThemeDB.FallbackFont;
        var text = Mathf.Round(Velocity.Length()).ToString();
        DrawString(font, end + DebugFontOffset, text, fontSize: DebugFontSize);
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (HitSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }

    private void OnDied() {
        QueueFree();
        if (DeathSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }

    Gun gun;
    float currentSpreadDegrees;

    public void EquipGun(GunData data) {
        if (gun != null) {
            gun.ReloadEnded -= OnReloadEnded;
        }

        gun = new Gun(data);
        gun.ReloadEnded += OnReloadEnded;
    }

    public void DoShot() {
        if (gun != null && gun.TryDoShot()) {
            SpawnBullet();

            var sfx = gun.Data.ShotSFX;
            if (sfx != null) {
                Audio.Instance.Play(sfx, Audio.BUS_SFX);
            }

            var data = gun.Data;
            currentSpreadDegrees += data.SpreadPerShot;
            currentSpreadDegrees = Mathf.Min(currentSpreadDegrees, data.SpreadMax);
            knockback.Add(GetGlobalMousePosition().DirectionTo(GlobalPosition), 100f);
        }
    }

    public void StartReload() {
        if (gun != null && gun.TryStartReload()) {
            var sfx = gun.Data.ReloadStartSFX;
            if (sfx != null) {
                Audio.Instance.Play(sfx, Audio.BUS_SFX);
            }
        }
    }

    private void OnReloadEnded() {
        var sfx = gun.Data.ReloadEndSFX;
        if (sfx != null) {
            Audio.Instance.Play(sfx, Audio.BUS_SFX);
        }
    }

    private void SpawnBullet() {
        if (gun.Data.BulletScene == null) {
            return;
        }

        var bullet = gun.Data.BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = GlobalPosition;

        bullet.Direction = GetSpreadDirection();
        bullet.Source = this;
        bullet.Damage = gun.Data.Damage;
        bullet.Lifetime = gun.Data.BulletLifetime;
        bullet.Speed = gun.Data.BulletSpeed;
        bullet.KnockbackForce = 100f; //TODO: заменить на статы

        GetTree().CurrentScene.AddChild(bullet);
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
