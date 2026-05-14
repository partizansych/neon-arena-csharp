using Godot;
using NeonArenaCsharp;

// Может быть у игрока и врагов
[GlobalClass]
public partial class WeaponController : Node2D {
    [Export] Loadout loadout;

    public Node2D Source { get; set; }

    public override void _Ready() {
        loadout.Equipped += OnLoadoutEquipped;
    }

    public void DoShot() {
        var weapon = loadout.GetCurrent();
        weapon?.DoShot();
    }

    public void StartReload() {
        var weapon = loadout.GetCurrent();
        weapon?.StartReload();
    }

    private void OnLoadoutEquipped(Loadout.Slot slot, Weapon weapon) {
        weapon.Shot += () => {
            SpawnSound(WeaponSound.Shot, weapon);
            SpawnBullet(weapon);
        };
        weapon.ReloadStarted += () => {
            SpawnSound(WeaponSound.ReloadStart, weapon);
        };
        weapon.ReloadEnded += () => {
            SpawnSound(WeaponSound.ReloadEnd, weapon);
        };
    }

    private void SpawnBullet(Weapon weapon) {
        var data = weapon.GetData();
        var bullet = data.BulletScene.Instantiate<Bullet>();

        bullet.GlobalPosition = GlobalPosition;
        bullet.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition());
        bullet.Source = Source;

        bullet.Lifetime = weapon.Get(AttributeType.BulletLifetime);
        bullet.Speed = weapon.Get(AttributeType.BulletSpeed);
        bullet.Damage = weapon.Get(AttributeType.Damage);

        GetTree().CurrentScene.AddChild(bullet);
    }

    private static void SpawnSound(WeaponSound type, Weapon weapon) {
        var data = weapon.GetData();
        if (data.Sounds.TryGetValue(type, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }
}
