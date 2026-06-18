using Godot;

[GlobalClass]
public partial class HealthSFX : Node2D {
    [Export] SimpleHealth health;
    [Export] public AudioStream DamageSFX;
    [Export] public AudioStream HealSFX;
    [Export] public AudioStream DeathSFX;

    public override void _Ready() {
        if (health == null) {
            GD.PushError($"[{Name}]: Ссылка на SimpleHealth не установлена. Логика озвучки отключена");
            return;
        }

        if (DamageSFX == null) {
            GD.PushWarning($"[{Name}]: Предупреждение. Не назначен звук 'DamageSFX'.");
        }

        if (HealSFX == null) {
            GD.PushWarning($"[{Name}]: Предупреждение. Не назначен звук 'HealSFX'.");
        }

        if (DeathSFX == null) {
            GD.PushWarning($"[{Name}]: Предупреждение. Не назначен звук 'DeathSFX'.");
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
        }
    }

    private void OnDied() {
        if (DeathSFX == null) return;
        Audio.Instance.PlayJuicySFX(DeathSFX);
    }
}
