using Godot;

namespace Stats;

[GlobalClass]
public partial class StatData : Resource {
    [Export] public StatType Type { get; private set; }
    [Export] public float BaseValue { get; private set; }
}
