using Godot;

public class ArenaState : IGameState {
    readonly IAppNavigation navigation;
    readonly IAppContext context;
    readonly PackedScene arenaScene;
    readonly PackedScene pauseMenuScene;

    Arena arena;
    PauseMenu pauseMenu;

    public ArenaState(IAppNavigation navigation, IAppContext context, PackedScene arenaScene, PackedScene pauseMenuScene) {
        this.navigation = navigation;
        this.context = context;
        this.arenaScene = arenaScene;
        this.pauseMenuScene = pauseMenuScene;
    }

    public void Enter() {
        PlaceArena();
        PlacePauseMenu();
    }

    public void Exit() {
        arena.QueueFree();
        pauseMenu.QueueFree();
    }

    public void Update(float delta) {
        if (Input.IsActionJustPressed("ui_cancel")) {
            if (pauseMenu.Visible) SetPauseMenuState(isVisible: false);
            else SetPauseMenuState(isVisible: true);
        }
    }

    void PlaceArena() {
        arena = arenaScene.Instantiate<Arena>();
        context.AddToLayer(arena, GameLayer.World);
    }

    void PlacePauseMenu() {
        pauseMenu = pauseMenuScene.Instantiate<PauseMenu>();
        context.AddToLayer(pauseMenu, GameLayer.Menu);
        pauseMenu.ResumeRequested += () => SetPauseMenuState(false);
        pauseMenu.ExitRequested += navigation.GoToMainMenu; ;
        SetPauseMenuState(isVisible: false);
    }

    void SetPauseMenuState(bool isVisible) {
        if (isVisible) pauseMenu.Show();
        else pauseMenu.Hide();
        pauseMenu.GetTree().Paused = isVisible;
    }
}
