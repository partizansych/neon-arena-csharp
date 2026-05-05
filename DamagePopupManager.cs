using Godot;

namespace NeonArenaCsharp;

public partial class DamagePopupManager : Node {
    [Export] PackedScene PopupScene;

    public override void _Ready() {
        var nodes = GetTree().GetNodesInGroup("healths");
        foreach (var node in nodes)
            if (node is Health health)
                ConnectToNode(health);
        GetTree().NodeAdded += OnNodeAdded;
    }

    public override void _ExitTree() {
        GetTree().NodeAdded -= OnNodeAdded;
    }

    private void ConnectToNode(Health health) {
        health.CurrentChanged += (oldValue, newValue) => {
            var popup = PopupScene.Instantiate<DamagePopup>();
            popup.BindDamage(oldValue - newValue);
            popup.GlobalPosition = health.GlobalPosition;
            AddChild(popup);
        };
    }

    private void OnNodeAdded(Node node) {
        if (node is Health health)
            ConnectToNode(health);
    }
}
