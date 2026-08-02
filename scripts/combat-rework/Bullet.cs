using Godot;

[GlobalClass]
public partial class Bullet : Area2D {
    [Export] StraightMovement2D movement;
    [Export] Lifetimer lifetimer;

    AttackContext ctx;
    int pierced;

    public void Initialize(AttackContext ctx) {
        this.ctx = ctx;
    }

    public override void _Ready() {
        BodyEntered += OnBodyEntered;

        movement.Direction = ctx.StartDirection;
        movement.Speed = ctx.Speed;
        lifetimer.Start(ctx.Duration);
    }

    private void OnBodyEntered(Node2D body) {
        if (body == ctx.Source) return;

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

                var hit = new ImpactContext(
                    victim: body,
                    point: GlobalPosition,
                    normal: -movement.Direction
                );

                foreach (var impact in ctx.Impacts)
                    impact.Apply(hit, ctx);

                if (pierced >= ctx.MaxTargets)
                    QueueFree();
            }
        }
    }
}
