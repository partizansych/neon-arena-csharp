using Godot;

namespace Movement;

public readonly record struct MoveOutput(Vector2 Velocity, bool IsActive = true) {
    public static MoveOutput Silenced => new(Vector2.Zero, false);
}

public enum MoveModType {
    Additive,   // Добавляется к текущей скорости (например, Input, Impulse)
    Override    // Полностью заменяет скорость (например, Dash, Stun)
}

[GlobalClass]
public abstract partial class MoveMod : Node {
    [Export] public MoveModType Type { get; private set; }
    [Export] public int Priority { get; private set; } = 0; // Чем выше, тем позже применяется

    public virtual void Update(float delta) { }
    public abstract MoveOutput Modify(float speed);
}
