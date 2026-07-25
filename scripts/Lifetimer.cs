using Godot;

[GlobalClass]
public partial class Lifetimer : Node {
    [Export] public Node Target { get; set;}
    [Export] public float Lifetime { get; set; } = 2.0f;
    [Export] public bool AutoStart { get; set; } = true;

    public override void _Ready() {
        Target ??= GetParent<Node>();
        if (AutoStart) Start(Lifetime);
    }

    public void Start(float time) {
        if (time <= 0) return;
        GetTree().CreateTimer(time).Timeout += OnTimeout;
    }

    private void OnTimeout() {
        Target.QueueFree();
    }
}
