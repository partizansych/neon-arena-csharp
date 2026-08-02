using Godot;

[GlobalClass]
public abstract partial class WeaponRule : Resource {
    public abstract void Execute(AttackContext ctx);
}
