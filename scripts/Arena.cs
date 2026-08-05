using Godot;

[GlobalClass]
public partial class Arena : Node2D {
    [Export] ProtostarSpawner protostarSpawner;
    [Export] Marker2D playerSpawnpoint;

    public Vector2 PlayerSpawnpoint => playerSpawnpoint.GlobalPosition;

    public void BindPlayerToSpawner(Node2D player) {
        protostarSpawner.Target = player;
    }

    public void BindRootToSpawner(Node root) {
        protostarSpawner.Container = root;
    }
}
