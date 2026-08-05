using Godot;

[GlobalClass]
public partial class MainMenu : Control {
    [Export] Button StartButton;
    [Export] Button ExitButton;

    public override void _Ready() {
        StartButton.Pressed += OnStart;
        ExitButton.Pressed += OnExit;
    }

    private void OnStart() {
        var _ = SceneManager.Instance.SwitchSceneAsync("res://scenes/gameplay.tscn");
    }

    private void OnExit() {
        GetTree().Quit();
    }
}
