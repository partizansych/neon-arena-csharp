using Godot;

[GlobalClass]
public partial class Main : Node, IAppContext, IAppNavigation {
    [Export] Node2D worldRoot;
    [Export] Control hudRoot;
    [Export] Control menuRoot;

    [Export] PackedScene mainMenuScene;
    [Export] PackedScene arenaScene;
    [Export] PackedScene pauseMenuScene;

    GameStateMachine gameStateMachine;

    public override void _Ready() {
        gameStateMachine = new GameStateMachine();
        GoToMainMenu();
    }

    public override void _Process(double delta) {
        gameStateMachine.Update((float)delta);
    }

    public void AddToLayer(Node node, GameLayer layer) {
        Node container = GetLayer(layer);
        container?.AddChild(node);
    }

    public void ClearLayer(GameLayer layer) {
        Node container = GetLayer(layer);
        if (container == null) return;

        foreach (var child in container.GetChildren()) {
            child.QueueFree();
        }
    }

    public void GoToMainMenu() {
        var mainMenuState = new MainMenuState(this, this, mainMenuScene);
        gameStateMachine.SwitchTo(mainMenuState);
    }

    public void GoToArena() {
        var arenaState = new ArenaState(this, this, arenaScene, pauseMenuScene);
        gameStateMachine.SwitchTo(arenaState);
    }

    public void ExitGame() {
        GetTree().Quit();
    }

    Node GetLayer(GameLayer layer) {
        return layer switch {
            GameLayer.World => worldRoot,
            GameLayer.Hud => hudRoot,
            GameLayer.Menu => menuRoot,
            _ => null
        };
    }
}
