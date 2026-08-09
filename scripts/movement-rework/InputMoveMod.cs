using Godot;

namespace Movement;

[GlobalClass]
public partial class InputMoveMod : MoveMod {
    public Vector2 Vector { get; private set; }

    public override void Update(float delta) {
        Vector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }

    public override MoveOutput Modify(float speed) {
        return new MoveOutput(Vector * speed);
    }
}
