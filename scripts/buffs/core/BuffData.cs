using Godot;

namespace Buffs;

[GlobalClass]
public partial class BuffData : Resource {
    [Export] public string Id { get; private set; }
    [Export] public Texture2D Icon { get; private set; }
    [Export] public float Duration { get; private set; }
    [Export] public int MaxStacks { get; private set; }
    [Export] public bool IsTickable { get; private set; }
    [Export] public float TickInterval { get; private set; }
    [Export] public BuffEffect[] Effects { get; private set; } = [];
}
