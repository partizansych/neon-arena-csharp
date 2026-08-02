using Godot;

[GlobalClass]
public partial class WeaponData : ItemData {
    [Export] public float BaseCooldown { get; private set; } = 1 / 4f;
    [Export] public float BasePower { get; private set; } = 1f;
    [Export] public float BaseSpeed { get; private set; } = 1f;
    [Export] public float BaseDuration { get; private set; } = 3f;
    [Export] public int BaseMaxTargets { get; private set; } = 1;
    [Export] public WeaponImpact[] Impacts { get; private set; }
    [Export] public WeaponRule[] Rules { get; private set; }
}
