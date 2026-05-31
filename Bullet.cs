using Godot;

[GlobalClass]
public partial class Bullet : Node2D {
    [Export] Area2D hitbox;
    [Export] Timer lifetimer;

    float speed = 0f;
    float damage = 1f;
    Vector2 direction = Vector2.Right;

    public Node2D Source { get; set; }
    public Gun Gun { get; set; }

    public float Speed {
        get => speed;
        set => speed = Mathf.Max(0f, value);
    }

    public float Lifetime {
        get => (float)lifetimer.WaitTime;
        set => lifetimer.WaitTime = Mathf.Max(0f, value);
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
        lifetimer.Timeout += QueueFree;
        lifetimer.Start();
    }

    public override void _PhysicsProcess(double delta) {
        Rotate(0.25f);
        GlobalPosition += direction * speed * (float)delta;
    }

    private void OnBodyEntered(Node2D body) {
        if (body is IDamageable) {
            Combat.Instance.Request(new DamageContext() {
                Source = Source,
                Target = body,
                HitPos = GlobalPosition,
                Weapon = Gun,
            });
        }

        QueueFree();
    }
}
