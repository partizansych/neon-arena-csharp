using Godot;

[GlobalClass]
public partial class Arena : Node2D {
    [Export] ProtostarSpawner protostarSpawner;
    [Export] Marker2D playerSpawnpoint;
    [Export] GunData playerGun;

    const string PlayerUID = "uid://dwwpawwnocksd";

    Player player;

    public override void _Ready() {
        PlacePlayer();

        protostarSpawner.Target = player;
        protostarSpawner.Container = this;
    }

    private void PlacePlayer() {
        var playerPacked = ResourceLoader.Load<PackedScene>(PlayerUID);
        player = playerPacked.Instantiate<Player>();
        AddChild(player);
        player.EquipGun(playerGun);
    }
}
