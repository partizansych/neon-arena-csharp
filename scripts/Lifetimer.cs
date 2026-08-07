using Godot;

[GlobalClass]
public partial class Lifetimer : Node {
    [Export] public Node Target { get; set;}
    [Export] public float Lifetime { get; set; } = 2.0f;
    [Export] public bool AutoStart { get; set; } = true;

    float timer;

    public override void _Ready() {
        Target ??= GetParent<Node>();
        if (AutoStart) Start(Lifetime);
    }

    public override void _Process(double delta) {
        if (timer > 0f) {
            timer -= (float)delta;
            if (timer <= 0f) {
                OnTimeout();
            }
        }
    }

    public void Start(float time) {
        if (time <= 0) return;
        timer = time;
    }

    private void OnTimeout() {
        Target.QueueFree();
    }
}
