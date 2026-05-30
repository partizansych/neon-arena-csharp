using Godot;

[GlobalClass]
public partial class Player : Character {
    [Export] GunController gunController;

    public override void _PhysicsProcess(double delta) {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * Get(CharacterStat.Speed);
        MoveAndSlide();

        if (Input.IsActionPressed("attack")) {
            gunController.DoShot();
        }
        if (Input.IsKeyPressed(Key.R)) {
            gunController.StartReload();
        }
    }

    public void EquipGun(GunData data) {
        gunController.Equip(data);
    }
}
