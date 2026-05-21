using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D, IDamageable {
    [Export] PlayerStatSheet stats;
    [Export] Health health;

    PlayerData data;
    Gun gun;

    public override void _Process(double delta) {
        if (gun != null && Input.IsActionPressed("attack")) {
            var mousePos = GetGlobalMousePosition();
            var direction = GlobalPosition.DirectionTo(mousePos);
            gun.DoShot(direction);
        }
    }

    public void Setup(PlayerData data) {
        this.data = data;
        stats.Setup(data);
    }

    public void EquipGun(Gun gun) {
        this.gun = gun;
        AddChild(gun);
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (data.TryGetSound(PlayerSound.Hit, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }
}
