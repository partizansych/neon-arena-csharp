using Godot;

public class Weapon {
    public WeaponData Data { get; }

    public float Cooldown;
    public float Power;
    public float Speed;
    public float Duration;
    public int MaxTargets;

    public float Timer { get; private set; }

    public Weapon(WeaponData data) {
        Data = data;

        Cooldown = data.BaseCooldown;
        Power = data.BasePower;
        Speed = data.BaseSpeed;
        Duration = data.BaseDuration;
        MaxTargets = data.BaseMaxTargets;
    }

    public void Update(float delta) {
        if (Timer > 0f) Timer -= delta;
    }

    public void TryAttack(Node2D src, Vector2 pos, Vector2 dir) {
        var ctx = CreateAttackContext(src, pos, dir);

        foreach (var rule in Data.Rules) {
            rule.Execute(ctx);
        }
    }

    AttackContext CreateAttackContext(Node2D src, Vector2 pos, Vector2 dir) {
        return new AttackContext {
            Source = src,
            StartPosition = pos,
            StartDirection = dir,
            Power = Power,
            Speed = Speed,
            Duration = Duration,
            MaxTargets = MaxTargets,
            Impacts = Data.Impacts
        };
    }
}
