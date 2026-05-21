using Godot;

[GlobalClass]
public partial class Gun : Node2D {
    [Export] Node2D muzzle;
    [Export] Timer shotTimer;
    [Export] Timer reloadTimer;
    [Export] GunStatSheet stats;

    GunData data;
    int ammo;

    public Node2D Source { get; set; }

    public override void _Ready() {
        SetupTimer(shotTimer);
        SetupTimer(reloadTimer);
    }

    public void Setup(GunData data) {
        this.data = data;
        stats.Setup(data);
        ammo = data.MaxAmmo;
    }

    public float Get(GunStat stat) {
        return stats.Get(stat);
    }

    public void DoShot(Vector2 direction) {
        if (!reloadTimer.IsStopped() || !shotTimer.IsStopped() || ammo <= 0) {
            return;
        }

        var fireRate = Get(GunStat.FireRate);
        shotTimer.Start(fireRate);
        ammo--;
        SpawnBullet(direction);
        SpawnSound(GunSound.Shot);
    }

    public void StartReload() {
        if (!reloadTimer.IsStopped()) {
            return;
        }

        var reloadTime = Get(GunStat.ReloadTime);
        reloadTimer.Start(reloadTime);
        SpawnSound(GunSound.ReloadStart);
    }

    private static void SetupTimer(Timer timer) {
        timer.Autostart = false;
        timer.OneShot = true;
        timer.IgnoreTimeScale = false;
    }

    private void SpawnBullet(Vector2 direction) {
        if (!data.TryCreateBullet(out var bullet)) {
            return;
        }

        bullet.GlobalPosition = muzzle.GlobalPosition;
        bullet.Direction = direction;
        bullet.Source = Source;

        bullet.Lifetime = Get(GunStat.BulletLifetime);
        bullet.Speed = Get(GunStat.BulletSpeed);
        bullet.Damage = Get(GunStat.Damage);

        GetTree().CurrentScene.AddChild(bullet);
    }

    private void SpawnSound(GunSound type) {
        if (data.TryGetSound(type, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }
}
