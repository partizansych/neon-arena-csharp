using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D, IDamageable {
    [Export] PlayerStatSheet stats;
    [Export] Health health;

    PlayerData data;

    public void Setup(PlayerData data) {
        this.data = data;
        stats.Setup(data);
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (data.TryGetSound(PlayerSound.Hit, out var sound)) {
            Audio.Instance.Play(sound, Audio.BUS_SFX);
        }
    }
}
