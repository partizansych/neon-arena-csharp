using Godot;

namespace Stats;

[GlobalClass]
public partial class ModifierData : Resource {
    [Export] public ModifierType Type { get; private set; }
    [Export] public float Value { get; private set; }

    public Modifier AsMod(object source) {
        return new Modifier(Value, Type, source);
    }
}
