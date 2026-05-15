using Godot;

[GlobalClass]
public partial class CharacterData : Resource {
    [Export] public float Speed { get; private set; }
    [Export] public float MaxHp { get; private set; }
}
