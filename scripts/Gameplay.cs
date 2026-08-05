using Godot;

[GlobalClass]
public partial class Gameplay : Node2D {
    [Export] PackedScene playerScene;
    [Export] Node entityRoot;
    [Export] Camera2D mainCamera;
    [Export] PauseMenu pauseMenu;
    [Export] Arena arena;

    Player player;

    public override void _Ready() {
        PlacePlayer(arena.PlayerSpawnpoint);
        arena.BindPlayerToSpawner(player);
        arena.BindRootToSpawner(entityRoot);

        pauseMenu.ResumeRequested += () => SetPauseMenuState(false);
        pauseMenu.ExitRequested += OnPauseMenuExitRequested;
    }

    public override void _ExitTree() {
        GetTree().Paused = false;
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event.IsActionPressed("ui_cancel")) {
            if (pauseMenu.Visible) SetPauseMenuState(isVisible: false);
            else SetPauseMenuState(isVisible: true);
        }
    }

    private void PlacePlayer(Vector2 pos) {
        player = playerScene.Instantiate<Player>();
        player.GlobalPosition = pos;
        entityRoot.AddChild(player);
    }

    private void SetPauseMenuState(bool isVisible) {
        if (isVisible) pauseMenu.Show();
        else pauseMenu.Hide();
        GetTree().Paused = isVisible;
    }

    private void OnPauseMenuExitRequested() {
        // TODO: Save
        var _ = SceneManager.Instance.SwitchSceneAsync("res://scenes/main_menu.tscn");
    }
}
