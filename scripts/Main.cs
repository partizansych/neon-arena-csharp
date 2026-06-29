using Godot;

[GlobalClass]
public partial class Main : Node {
    [Export] Node2D worldRoot;
    [Export] Control hudRoot;

    const string ArenaLevelUID = "uid://cn8csvm0p4o6d";

    public override void _Ready() {
        var arenaPacked = ResourceLoader.Load<PackedScene>(ArenaLevelUID);
        var arena = arenaPacked.Instantiate();
        worldRoot.AddChild(arena);
    }
}
