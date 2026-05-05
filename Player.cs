using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class Player : Character {
    [Export] WeaponController weaponController;

    public override void _PhysicsProcess(double delta) {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * Stats.Speed.Value;
        MoveAndSlide();

        if (Input.IsActionPressed("attack")) {
            weaponController.IsShooting = true;
            weaponController.ShootDirection = GlobalPosition.DirectionTo(GetGlobalMousePosition());
        }
        else {
            weaponController.IsShooting = false;
        }

        if (Input.IsActionJustPressed("reload")) {
            weaponController.Reload();
        }
    }
}
