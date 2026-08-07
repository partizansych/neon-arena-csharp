using Godot;

[GlobalClass]
public partial class DamagePopup : Node2D {
    [Export] Label label;

    public void BindDamage(float damage) {
        label.Text = damage.ToString();
    }

    public override void _Ready() {
        var tween = CreateTween();

        // --- 1. ПОЯВЛЕНИЕ (Pop & Bounce) ---
        // Pop & Bounce: Scale from Zero to One
        tween.TweenProperty(this, "scale", Vector2.One, 0.4f)
            .From(Vector2.Zero)
            .SetTrans(Tween.TransitionType.Back)
            .SetEase(Tween.EaseType.Out);

        // Движение вверх параллельно (Relative Y -60)
        tween.Parallel().TweenProperty(this, "position:y", -60.0f, 0.4f)
            .AsRelative()
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.Out);

        // --- 2. ЗАВИСАНИЕ (Float & Pulse) ---

        // Цикл пульсации прозрачности
        for (int i = 0; i < 2; i++) {
            tween.TweenProperty(this, "modulate:a", 0.65f, 0.2f);
            tween.TweenProperty(this, "modulate:a", 1.0f, 0.2f);
        }

        // --- 3. ИСЧЕЗНОВЕНИЕ (Fade & Shrink) ---

        // Быстрое исчезновение масштаба
        tween.TweenProperty(this, "scale", Vector2.Zero, 0.2f)
            .SetTrans(Tween.TransitionType.Cubic)
            .SetEase(Tween.EaseType.In);

        // Параллельно: прозрачность в 0
        tween.Parallel().TweenProperty(this, "modulate:a", 0.0f, 0.2f);

        // Параллельно: еще немного вверх (как дым)
        tween.Parallel().TweenProperty(this, "position:y", -30.0f, 0.2f)
            .AsRelative();

        // Удаляем узел после завершения твина
        tween.TweenCallback(Callable.From(QueueFree));
    }
}
