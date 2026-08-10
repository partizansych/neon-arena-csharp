using System.Linq;
using Godot;
using Stats;

namespace Movement;

[GlobalClass]
public partial class CharacterMover : Node {
    [Export] CharacterBody2D body;
    [Export] StatContainer stats;
    [Export] MoveMod[] mods;

    public override void _Ready() {
        mods = [.. mods.OrderBy(m => m.Priority)];
    }

    public override void _PhysicsProcess(double delta) {
        foreach (var modifier in mods) {
            modifier.Update((float)delta);
        }

        Vector2 velocity = Vector2.Zero;
        float speed = stats.GetValue(StatType.Speed);

        foreach (var mod in mods) {
            var modOutput = mod.Modify(speed);

            if (!modOutput.IsActive) {
                continue;
            }

            switch (mod.Type) {
                case MoveModType.Additive:
                    velocity += modOutput.Velocity;
                    break;
                case MoveModType.Override:
                    velocity = modOutput.Velocity;
                    break;
            }
        }

        body.Velocity = velocity;
        body.MoveAndSlide();
    }
}
