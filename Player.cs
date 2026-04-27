using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 200f;
    [Export] public PackedScene BulletScene;

    public override void _PhysicsProcess(double delta)
    {
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = direction * Speed;
        MoveAndSlide();

        if (Input.IsActionJustPressed("ui_accept"))
        {
            var bullet = BulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = GlobalPosition;
            bullet.Direction = Vector2.Down;
            GetTree().CurrentScene.AddChild(bullet);
        }
    }
}
