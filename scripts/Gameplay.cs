using Godot;

[GlobalClass]
public partial class Gameplay : Node2D {
    [Export] PackedScene playerScene;
    [Export] Node pausableContainer;
    [Export] Camera2D mainCamera;
    [Export] PauseMenu pauseMenu;
    [Export] Arena arena;

    CharacterBody2D player;

    public override void _Ready() {
        PlacePlayer(arena.PlayerSpawnpoint);
        arena.BindPlayerToSpawner(player);
        arena.BindRootToSpawner(pausableContainer);

        pauseMenu.ResumeRequested += () => SetPauseMenuState(false);
        pauseMenu.ExitRequested += OnPauseMenuExitRequested;
        Event.Instance.NodeSpawned += OnNodeSpawned;
    }

    public override void _ExitTree() {
        GetTree().Paused = false;

        Event.Instance.NodeSpawned -= OnNodeSpawned;
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event.IsActionPressed("ui_cancel")) {
            if (pauseMenu.Visible) SetPauseMenuState(isVisible: false);
            else SetPauseMenuState(isVisible: true);
        }
    }

    private void PlacePlayer(Vector2 pos) {
        player = playerScene.Instantiate<CharacterBody2D>();
        player.GlobalPosition = pos;
        pausableContainer.AddChild(player);
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

    private void OnNodeSpawned(Node node) {
        pausableContainer.AddChild(node);
    }
}
