using Godot;

[GlobalClass]
public abstract partial class MovementModifier : Node {
    public abstract void Modify(ref Vector2 velocity);
}
