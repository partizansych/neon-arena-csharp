namespace Movement;

using Godot;

[GlobalClass]
public abstract partial class MoveMod : Node {
    public enum ModType {
        Additive,   // Добавляется к текущей скорости (например, Input, Impulse)
        Override    // Полностью заменяет скорость (например, Dash, Stun)
    }

    [Export] public ModType Type { get; private set; }
    [Export] public int Priority { get; private set; } = 0; // Чем выше, тем позже применяется

    public virtual void Update(float delta) { }
    public abstract Vector2 Modify(Vector2 vel);
}
