using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 200f;
    [Export] public PackedScene BulletScene;

    public override void _PhysicsProcess(double delta)
    {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = direction * Speed;
        MoveAndSlide();

        if (Input.IsActionJustPressed("attack"))
        {
            var bullet = BulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = GlobalPosition;
            bullet.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition());
            GetTree().CurrentScene.AddChild(bullet);
        }
    }
}
