using Godot;

namespace Movement;

[GlobalClass]
public partial class InputVector : MoveMod {
    [Export] public float Speed { get; set; } = 200f;

    public Vector2 Vector { get; private set; }

    public override void Update(float delta) {
        Vector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }

    public override Vector2 Modify(Vector2 vel) {
        return Vector * Speed;
    }
}
