using Godot;

public readonly struct ImpactContext(Node2D victim, Vector2 point, Vector2 normal)
{
    public Node2D Victim { get; } = victim;
    public Vector2 HitPoint { get; } = point;
    public Vector2 HitNormal { get; } = normal;
}
