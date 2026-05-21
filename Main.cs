using Godot;

[GlobalClass]
public partial class Main : Node2D {
    [ExportGroup("Первоначальные оружия")]
    [Export] PackedScene gunScene;
    [Export] GunData primary;
    [Export] GunData heavy;

    [ExportGroup("Настройка игрока")]
    [Export] PackedScene playerScene;
    [Export] PlayerData playerData;

    public override void _Ready() {
        var player = CreatePlayer();
        player.EquipGun(CreateGun());
    }

    private Player CreatePlayer() {
        var player = playerScene.Instantiate<Player>();
        player.Setup(playerData);
        AddChild(player);
        return player;
    }

    private Gun CreateGun() {
        var gun = gunScene.Instantiate<Gun>();
        gun.Setup(primary);
        return gun;
    }
}
