using Godot;

[GlobalClass]
public partial class SpreadComponent : Node2D {
    [Export] public float Min = 0f;
    [Export] public float Max = 12f;
    [Export] public float RecoveryRate = 15f;

    [ExportGroup("Debug")]
    [Export] public bool Debug;
    [Export] public Color DebugColor = Colors.Purple;
    [Export] public float DebugMaxLineLength = 100f;

    float currentSpread;

    public override void _PhysicsProcess(double delta) {
        if (currentSpread <= Min) return;
        currentSpread -= RecoveryRate * (float)delta;
        currentSpread = Mathf.Max(currentSpread, Min);

        if (Debug) QueueRedraw();
    }

    public override void _Draw() {
        if (!Debug) return;
        if (currentSpread <= 0f) return;

        var halfSpreadRad = Mathf.DegToRad(currentSpread / 2f);
        Vector2 leftDirection = Vector2.Right.Rotated(-halfSpreadRad);
        Vector2 rightDirection = Vector2.Right.Rotated(halfSpreadRad);

        DrawLine(Vector2.Zero, leftDirection * DebugMaxLineLength, DebugColor, 1.5f);
        DrawLine(Vector2.Zero, rightDirection * DebugMaxLineLength, DebugColor, 1.5f);
    }

    public void Reset() {
        currentSpread = Min;
    }

    public void Apply(float amount) {
        currentSpread += amount;
        currentSpread = Mathf.Min(currentSpread, Max);
    }

    public Vector2 ModifyDirection(Vector2 direction) {
        if (currentSpread <= 0f) return direction;

        // Половина угла в радианах (максимальное отклонение в одну сторону)
        float maxOffsetRad = Mathf.DegToRad(currentSpread / 2f);
        // Задаем стандартное отклонение так, чтобы край конуса приходился на 3 сигмы.
        // Это гарантирует, что 99.7% пуль попадут внутрь дебаг-линий без циклов do-while.
        float sigma = maxOffsetRad / 3f;
        float offset = (float)GD.Randfn(0f, sigma);

        // На всякий случай жестко зажмем в лимиты (вместо тяжелого цикла)
        offset = Mathf.Clamp(offset, -maxOffsetRad, maxOffsetRad);

        return direction.Rotated(offset);
    }
}
