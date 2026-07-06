using System;
using Godot;

[GlobalClass]
public partial class MainMenu : Control {
    public event Action StartGameRequested;
    public event Action ExitGameRequested;

    [Export] Button StartButton;
    [Export] Button ExitButton;

    public override void _Ready() {
        StartButton.Pressed += () => StartGameRequested?.Invoke();
        ExitButton.Pressed += () => ExitGameRequested?.Invoke();
    }
}
