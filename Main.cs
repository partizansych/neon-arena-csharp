using Godot;

[GlobalClass]
public partial class Main : Node2D {
    [Export] public float TimeScale = 1f;

    [ExportGroup("Первоначальные оружия")]
    [Export] PackedScene gunScene;
    [Export] GunData primary;
    [Export] GunData heavy;

    [ExportGroup("Настройка игрока")]
    [Export] PackedScene playerScene;

    [ExportGroup("")]
    [Export] WaveManager waveManager;

    public override void _Ready() {
        Engine.TimeScale = TimeScale;

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
