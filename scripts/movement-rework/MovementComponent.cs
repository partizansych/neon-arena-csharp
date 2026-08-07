using Godot;

[GlobalClass]
public partial class MovementComponent : Node {
    [Export] CharacterBody2D body;
    [Export] MovementModifier[] modifiers;

    public override void _PhysicsProcess(double delta) {
        Vector2 velocity = Vector2.Zero;

        foreach (var modifier in modifiers) {
            modifier.Modify(ref velocity);
        }

        body.Velocity = velocity;
        body.MoveAndSlide();
    }
}
