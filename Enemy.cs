using Godot;

[GlobalClass]
public partial class Enemy : CharacterBody2D {
    [Signal] public delegate void DiedEventHandler();

    [Export] public float Hp = 100f;

    public void TakeDamage(float amount) {
        Hp -= amount;
        if (Hp <= 0f)
            Die();
    }

    public void Die() {
        QueueFree();
        EmitSignal(SignalName.Died);
    }
}
