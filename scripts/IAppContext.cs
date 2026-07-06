using Godot;

public interface IAppContext {
    void AddToLayer(Node node, GameLayer layer);
    void ClearLayer(GameLayer layer);
}
