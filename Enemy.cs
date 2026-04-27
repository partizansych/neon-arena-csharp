using Godot;

public partial class Enemy : CharacterBody2D
{
    [Export] public float Hp = 100f;

    public void TakeDamage(float amount)
    {
        Hp -= amount;
        if (Hp <= 0f)
            QueueFree();
    }
}
