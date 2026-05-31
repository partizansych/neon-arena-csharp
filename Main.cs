using Godot;

[GlobalClass]
public partial class Main : Node2D {
    [ExportGroup("Первоначальные оружия")]
    [Export] PackedScene gunScene;
    [Export] GunData primary;
    [Export] GunData heavy;

    [ExportGroup("Настройка игрока")]
    [Export] PackedScene playerScene;

    [ExportGroup("")]
    [Export] WaveManager waveManager;

    public override void _Ready() {
        var player = CreatePlayer();
        player.EquipGun(primary);

        waveManager.Player = player;
        waveManager.StartWave();
    }

    private Player CreatePlayer() {
        var player = playerScene.Instantiate<Player>();
        AddChild(player);
        return player;
    }
}
