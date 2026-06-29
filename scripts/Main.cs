using Godot;

[GlobalClass]
public partial class Main : Node {
    [Export] Node2D worldRoot;
    [Export] Control hudRoot;
    [Export] Control menuRoot;

    const string ArenaLevelUID = "uid://cn8csvm0p4o6d";
    const string PauseMenuUID = "uid://yfp0fhs7qda7";

    Arena arena;
    Control pauseMenu;

    public override void _Ready() {
        PlaceArena();
        PlacePauseMenu();
    }

    public override void _Process(double delta) {
        if (Input.IsActionJustPressed("ui_cancel")) {
            if (pauseMenu.Visible) SetPauseMenuState(isVisible: false);
            else SetPauseMenuState(isVisible: true);
        }
    }

    private void PlaceArena() {
        var arenaPacked = ResourceLoader.Load<PackedScene>(ArenaLevelUID);
        arena = arenaPacked.Instantiate<Arena>();
        worldRoot.AddChild(arena);
    }

    private void PlacePauseMenu() {
        var menuPacked = ResourceLoader.Load<PackedScene>(PauseMenuUID);
        pauseMenu = menuPacked.Instantiate<Control>();
        menuRoot.AddChild(pauseMenu);

        SetPauseMenuState(isVisible: false);
    }

    private void SetPauseMenuState(bool isVisible) {
        if (isVisible) pauseMenu.Show();
        else pauseMenu.Hide();
        pauseMenu.GetTree().Paused = isVisible;
    }
}
