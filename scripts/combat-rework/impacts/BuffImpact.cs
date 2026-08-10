using Buffs;
using Godot;

[GlobalClass]
public partial class BuffImpact : WeaponImpact {
    [Export] public BuffData[] Buffs { get; private set; } = [];

    public override void Apply(ImpactContext impact, AttackContext attack) {
        var component = impact.Victim.GetComponent<BuffComponent>();
        if (component != null) {
            foreach (var buff in Buffs) {
                component.Add(buff);
            }
        }
    }
}
