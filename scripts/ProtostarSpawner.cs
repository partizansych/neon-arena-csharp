using Godot;

[GlobalClass]
public partial class ProtostarSpawner : Node2D {
    [Export] public PackedScene ProtostarScene;
    [Export] public float Delay = 1f;
    [Export] public bool AutoStart = true;

    public Node2D Target { get; set; }
    public Node Container { get; set; }
    public bool IsActive { get; set; }

    public bool CanSpawn => IsActive && Target != null;

    float spawnerTimer;

    public override void _Ready() {
        spawnerTimer = Delay;
        Container ??= this;
        if (AutoStart) IsActive = true;
    }

    public override void _PhysicsProcess(double delta) {
        if (!CanSpawn) return;

        if (spawnerTimer <= 0f) {
            PlaceProtostar();
            spawnerTimer = Delay;
        }
        else spawnerTimer -= (float)delta;
    }

    private void PlaceProtostar() {
        var protostar = ProtostarScene.Instantiate<Protostar>();
        protostar.GlobalPosition = GetRandomPos(GlobalPosition);
        protostar.Target = Target;
        Container.AddChild(protostar);
    }

    private Vector2 GetRandomPos(Vector2 origin) {
        var viewportHalf = GetViewport().GetVisibleRect().Size / 2;
        return new Vector2(
            (float)GD.RandRange(origin.X - viewportHalf.X, origin.X + viewportHalf.X),
            (float)GD.RandRange(origin.Y - viewportHalf.Y, origin.Y + viewportHalf.Y)
        );
    }
}
