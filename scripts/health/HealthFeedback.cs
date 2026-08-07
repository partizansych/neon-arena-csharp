using Godot;

[GlobalClass]
public partial class HealthFeedback : Node2D {
    [Export] SimpleHealth health;
    [Export] public AudioStream DamageSFX;
    [Export] public AudioStream HealSFX;
    [Export] public AudioStream DeathSFX;

    public override void _Ready() {
        if (health == null) {
            GD.PushError($"Ссылка на 'SimpleHealth' не установлена.");
            return;
        }

        if (DamageSFX == null) {
            GD.PushWarning($"Не назначен звук 'DamageSFX'.");
        }

        if (HealSFX == null) {
            GD.PushWarning($"Не назначен звук 'HealSFX'.");
        }

        if (DeathSFX == null) {
            GD.PushWarning($"Не назначен звук 'DeathSFX'.");
        }

        health.CurrentChanged += OnHpChanged;
        health.Died += OnDied;
    }

    private void OnHpChanged(float oldValue, float newValue) {
        if (newValue > oldValue) {
            if (HealSFX == null) return;
            Audio.Instance.PlayJuicySFX(HealSFX);
        }
        else if (newValue < oldValue) {
            if (DamageSFX == null) return;
            Audio.Instance.PlayJuicySFX(DamageSFX);
            Event.Instance.Damaged.Invoke(oldValue - newValue, GlobalPosition);
        }
    }

    private void OnDied() {
        if (DeathSFX == null) return;
        Audio.Instance.PlayJuicySFX(DeathSFX);
    }
}
