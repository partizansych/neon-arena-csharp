using Godot;

[GlobalClass]
public partial class WeaponTester : Node2D {
    [Export] Node2D source;
    [Export] WeaponData initialWeaponData;

    Weapon weapon;

    public override void _Ready() {
        weapon = new Weapon(initialWeaponData);
    }

    public override void _Process(double delta) {
        weapon.Update((float)delta);

        if (Input.IsActionJustPressed("attack")) {
            weapon.TryAttack(source, GlobalPosition, GetDirectionToMouse());
        }
    }

    Vector2 GetDirectionToMouse() {
        var mousePos = GetGlobalMousePosition();
        return GlobalPosition.DirectionTo(mousePos);
    }
}
