using Godot;

[GlobalClass]
public partial class DamagePopupManager : Node2D {
    [Export] PackedScene DamagePopupScene;
    [Export] Node Container;

    public override void _Ready() {
        Event.Instance.Damaged += OnDamaged;
    }

    public override void _ExitTree() {
        Event.Instance.Damaged -= OnDamaged;
    }

    private void OnDamaged(float amount, Vector2 position) {
        var popup = DamagePopupScene.Instantiate<DamagePopup>();
        popup.BindDamage(amount);
        popup.GlobalPosition = position;
        Container.AddChild(popup);
    }
}
