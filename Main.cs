using Godot;

[GlobalClass]
public partial class Main : Node2D {
    [ExportGroup("Первоначальные оружия")]
    [Export] GunData primary;
    [Export] GunData heavy;

    [ExportGroup("Настройка игрока")]
    [Export] PackedScene playerScene;
    [Export] PlayerData playerData;

    public override void _Ready() {
        SpawnPlayer();
    }

    private void SpawnPlayer() {
        var player = playerScene.Instantiate<Player>();
        player.Setup(playerData);
        AddChild(player);
    }
}
