using Godot;

[GlobalClass]
public partial class WeaponData : ItemData {
  [Export] public float Cooldown { get; private set; } = 1 / 4f;
  [Export] public float BasePower { get; private set; } = 1f;
  [Export] public float BaseSpeed { get; private set; } = 1f;
  [Export] public float BaseDuration { get; private set; } = 3f;
  [Export] public int BaseMaxTargets { get; private set; } = 1;
  [Export] public WeaponModuleData[] Modules { get; private set; }
}
