using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class Bullet : Node2D, IProjectile {
    [Export] Area2D hitbox;
    [Export] Timer lifetimer;

    float speed;
    float lifetime;
    float damage;
    Vector2 direction = Vector2.Right;

    public Node2D Source { get; set; }

    public float Speed {
        get => speed;
        set => speed = Mathf.Max(0f, value);
    }

    public float Lifetime {
        get => lifetime;
        set => lifetime = Mathf.Max(0f, value);
    }

    public float Damage {
        get => damage;
        set => damage = Mathf.Max(0f, value);
    }

    public Vector2 Direction {
        get => direction;
        set {
            if (value != Vector2.Zero) {
                direction = value;
            }
        }
    }

    public override void _Ready() {
        hitbox.BodyEntered += OnBodyEntered;

        lifetimer.WaitTime = lifetime;
        lifetimer.Timeout += QueueFree;
        lifetimer.Start();
    }

    public override void _PhysicsProcess(double delta) {
        GlobalPosition += direction * speed * (float)delta;
    }

    private void OnBodyEntered(Node2D body) {
        Combat.Instance.Request(new DamageContext() {
            Source = Source,
            Target = body,
            Damage = damage
        });

        QueueFree();
    }
}
