using Godot;
using Movement;

[GlobalClass]
public partial class DashSFX : Node2D {
    [Export] DashMoveMod dash;
    [Export] AudioStream StartSFX;
    [Export] AudioStream FinishSFX;

    public override void _Ready() {

        // КРИТИЧЕСКАЯ ОШИБКА (Error)
        // Без компонента рывка скрипт полностью бесполезен. Выдаем ошибку и останавливаемся.
        if (dash == null) {
            GD.PushError($"Ссылка на DashComponent не установлена.");
            return;
        }

        // МЯГКИЕ ПРЕДУПРЕЖДЕНИЯ (Warnings)
        // Если звуков нет — это не сломает игру, но разработчик должен знать, что рывок будет «немым».
        if (StartSFX == null) {
            GD.PushWarning($"Не назначен звук 'StartSFX'");
        }

        if (FinishSFX == null) {
            GD.PushWarning($"Не назначен звук 'FinishSFX'.");
        }

        dash.Started += OnDashStarted;
        dash.Finished += OnDashFinished;
    }

    private void OnDashStarted() {
        if (StartSFX == null) return;
        Audio.Instance.PlayJuicySFX(StartSFX);
    }

    private void OnDashFinished() {
        if (FinishSFX == null) return;
        Audio.Instance.PlayJuicySFX(FinishSFX);
    }
}
