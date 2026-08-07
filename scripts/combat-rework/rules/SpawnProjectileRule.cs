using Godot;

[GlobalClass]
public partial class SpawnProjectileRule : WeaponRule {
    [Export] public PackedScene ProjectileScene { get; private set; }

    public override void Execute(AttackContext ctx) {
        var projectile = ProjectileScene.Instantiate<Bullet>();
        projectile.GlobalPosition = ctx.StartPosition;
        projectile.Initialize(ctx);
        Event.Instance.NodeSpawned.Invoke(projectile);
    }
}
