using Godot;
using NeonArenaCsharp;

[GlobalClass]
public partial class Player : CharacterBody2D {
    [Export] public float Speed = 200f;
    [Export] public WeaponController WeaponController;

    public override void _PhysicsProcess(double delta) {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * Speed;
        MoveAndSlide();

        if (Input.IsActionPressed("attack")) {
            WeaponController.isShooting = true;
            WeaponController.ShootDirection = GlobalPosition.DirectionTo(GetGlobalMousePosition());
        }
        else {
            WeaponController.isShooting = false;
        }

        if (Input.IsActionJustPressed("reload")) {
            WeaponController.Reload();
        }
    }
}
