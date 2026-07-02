using System;
using Godot;

public partial class Event : Node {
    public static Event Instance { get; private set; }

    public Action<float, Vector2> Damaged = delegate { };
    public Action EnemyDied = delegate { };
    public Action PlayerDied = delegate { };

    public override void _Ready() {
        if (Instance == null) {
            Instance = this;
            return;
        }
        QueueFree();
    }
}
