using Godot;

[GlobalClass]
public partial class DashSFX : Node2D {
    [Export] DashComponent dash;
    [Export] AudioStream StartSFX;
    [Export] AudioStream FinishSFX;

    public override void _Ready() {

        // КРИТИЧЕСКАЯ ОШИБКА (Error)
        // Без компонента рывка скрипт полностью бесполезен. Выдаем ошибку и останавливаемся.
        if (dash == null) {
            GD.PushError($"[{Name}]: Ссылка на DashComponent не установлена. Логика озвучки отключена");
            return;
        }

        // МЯГКИЕ ПРЕДУПРЕЖДЕНИЯ (Warnings)
        // Если звуков нет — это не сломает игру, но разработчик должен знать, что рывок будет «немым».
        if (StartSFX == null) {
            GD.PushWarning($"[{Name}]: Предупреждение. Не назначен звук 'StartSFX'. Начало рывка будет беззвучным.");
        }

        if (FinishSFX == null) {
            GD.PushWarning($"[{Name}]: Предупреждение. Не назначен звук 'FinishSFX'. Конец рывка будет беззвучным.");
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
