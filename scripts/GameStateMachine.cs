#nullable enable
public class GameStateMachine {
    IGameState? current;

    public void SwitchTo(IGameState newState) {
        current?.Exit();
        current = newState;
        current?.Enter();
    }

    public void Update(float delta) {
        current?.Update(delta);
    }
}
#nullable disable
