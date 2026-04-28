using Godot;

public partial class Bullet : Area2D {
    [Export] public float Speed = 400f;
    public Vector2 Direction = Vector2.Right;

    public override void _Ready() {
        BodyEntered += OnBodyEntered;
        GetTree().CreateTimer(2.0f).Timeout += QueueFree;
    }

    public override void _PhysicsProcess(double delta) {
        GlobalPosition += Direction * Speed * (float)delta;
    }

    private void OnBodyEntered(Node2D body) {
        if (body is Player)
            return;
        else if (body is Enemy enemy)
            enemy.TakeDamage(50f);

        QueueFree();
    }
}