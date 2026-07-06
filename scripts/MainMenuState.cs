using Godot;

public class MainMenuState : IGameState {
    readonly IAppNavigation navigation;
    readonly IAppContext context;
    readonly PackedScene menuScene;

    MainMenu menu;

    public MainMenuState(IAppNavigation navigation, IAppContext context, PackedScene menuScene) {
        this.navigation = navigation;
        this.context = context;
        this.menuScene = menuScene;
    }

    public void Enter() => PlaceMenuScene();
    public void Exit() => menu.QueueFree();
    public void Update(float detla) { }

    void PlaceMenuScene() {
        menu = menuScene.Instantiate<MainMenu>();
        context.AddToLayer(menu, GameLayer.Menu);
        menu.ExitGameRequested += CloseGame;
        menu.StartGameRequested += StartGame;
    }

    void StartGame() => navigation.GoToArena();
    void CloseGame() => navigation.ExitGame();

}
