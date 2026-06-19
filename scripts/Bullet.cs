using Godot;

[GlobalClass]
public partial class Bullet : Node2D {
    [Export] Area2D hitbox;
    [Export] Timer lifetimer;

    public Node2D Source;
    public float Speed = 0f;
    public float Lifetime = 1f;
    public float Damage = 1f;
    public float KnockbackForce = 0f;
    public Vector2 Direction = Vector2.Right;

    public override void _Ready() {
        hitbox.BodyEntered += OnBodyEntered;
        lifetimer.Timeout += QueueFree;
        lifetimer.Start(Lifetime);
    }

    public override void _PhysicsProcess(double delta) {
        Rotate(0.25f);
        GlobalPosition += Direction * Speed * (float)delta;
    }

    private void OnBodyEntered(Node2D body) {
        if (body is IHittable hittable) {
            hittable.Hit(new HitInfo {
                Source = Source,
                HitPoint = GlobalPosition,
                Type = HitType.Damage,
                Value = Damage,
                KnockbackForce = KnockbackForce,
            });
        }

        QueueFree();
    }
}
