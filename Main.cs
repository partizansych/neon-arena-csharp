using Godot;
using NeonArenaCsharp;

public partial class Main : Node2D {
    [ExportGroup("Настройки игрока")]
    [Export] Player player;
    [Export] WeaponData primary;
    [Export] WeaponData heavy;

    public override void _Ready() {
        player.Equip(Loadout.Slot.Primary, primary);
        player.Equip(Loadout.Slot.Heavy, heavy);
    }
}
