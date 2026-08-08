using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class Hitbox : Area2D {
    [Export] public float Interval { get; set; } = 0.5f;
    [Export] public float Damage { get; set; } = 10f;

    readonly List<Node2D> targets = new(16);
    float timer;

    public override void _Ready() {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
        timer = Interval;
    }

    public override void _PhysicsProcess(double delta)
    {
        timer += (float)delta;

        if (timer >= Interval && targets.Count > 0) {
            timer -= Interval;
            OnTick();
        }
        else if (timer >= Interval && targets.Count == 0) {
            timer = Interval;
        }
    }

    private void OnBodyEntered(Node2D body) {
        if (!targets.Contains(body)) {
            targets.Add(body);
        }
    }

    private void OnBodyExited(Node2D body) {
        targets.Remove(body);
    }

    private void OnTick() {
        for (int i = targets.Count - 1; i >= 0; i--) {
            var target = targets[i];

            if (!IsInstanceValid(target)) {
                targets.RemoveAt(i);
                continue;
            }

            if (target.TryGetComponent<SimpleHealth>(out var health)) {
                health.Reduce(Damage);
            }
        }
    }
}
