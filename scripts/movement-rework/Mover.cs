namespace Movement;

using System.Linq;
using Godot;

[GlobalClass]
public partial class Mover : Node {
    [Export] CharacterBody2D body;
    [Export] MoveMod[] modifiers;

    public override void _Ready() {
        modifiers = [.. modifiers.OrderBy(m => m.Priority)];
    }

    public override void _PhysicsProcess(double delta) {
        Vector2 velocity = Vector2.Zero;

        foreach (var modifier in modifiers) {
            modifier.Update((float)delta);
        }

        foreach (var modifier in modifiers) {
            if (modifier.Type == MoveMod.ModType.Override) {
                velocity = modifier.Modify(velocity);
            }
            else velocity += modifier.Modify(velocity);
        }

        body.Velocity = velocity;
        body.MoveAndSlide();
    }
}
