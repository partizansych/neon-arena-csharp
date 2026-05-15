using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class Player : Character {
    [Export] Loadout loadout;
    [Export] WeaponController weaponController;

    public override void _PhysicsProcess(double delta) {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * Get(StatType.Speed);
        MoveAndSlide();

        if (Input.IsKeyPressed(Key.Key1)) {
            loadout.SwitchTo(Loadout.Slot.Primary);
        }
        else if (Input.IsKeyPressed(Key.Key2)) {
            loadout.SwitchTo(Loadout.Slot.Heavy);
        }

        if (Input.IsActionPressed("attack")) {
            weaponController.DoShot();
        }
    }

    public void Equip(Loadout.Slot slot, WeaponData data) {
        loadout.Equip(slot, data);
    }
}
