using System.Collections.Generic;
using Godot;

public struct BulletContext {
    public Node2D Source;
    public Vector2 Direction;
    public float Speed;
    public float Lifetime;
    public int MaxTargets;
    public IReadOnlyList<IImpactEffect> ImpactEffects;

    public readonly Vector2 Velocity => Direction * Speed;
}

[GlobalClass]
public partial class Bullet : Area2D {
    [Export] StraightMovement2D movement;
    [Export] LifeTimer lifetimer;

    BulletContext context;
    int pierced;

    public void Initialize(BulletContext context) {
        this.context = context;
    }

    public override void _Ready() {
        BodyEntered += OnBodyEntered;

        lifetimer.Start(context.Lifetime);

        movement.Direction = context.Direction;
        movement.Speed = context.Speed;
    }

    private void OnBodyEntered(Node2D body) {
        if (body == context.Source) return;

        if (body is CollisionObject2D collider) {

            if (collider.GetCollisionLayerValue(1)) {
                QueueFree();
                return;
            }

            // TODO: Сделать что-нибудь со слоями
            // - убрать хардкод цифры
            // - сделать слой Entity
            if (collider.GetCollisionLayerValue(3)) {
                pierced++;

                var hit = new HitContext(
                    source: context.Source,
                    victim: body,
                    point: GlobalPosition,
                    normal: context.Direction
                );

                foreach (var effect in context.ImpactEffects)
                    effect.Apply(hit);

                if (pierced >= context.MaxTargets)
                    QueueFree();
            }
        }
    }
}
