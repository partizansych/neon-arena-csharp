using Godot;

[GlobalClass]
public partial class ItemData : Resource {
    [Export] public string ID { get; private set; }
    // [Export] public string Name { get; private set; }
    [Export] public Texture2D Icon { get; private set; }
}
