using System;
using Godot;

[GlobalClass]
public partial class PauseMenu : Control {
    public event Action ResumeRequested;
    public event Action ExitRequested;

    [Export] Button resumeButton;
    [Export] Button settingsButton;
    [Export] Button exitButton;

    public override void _Ready() {
        resumeButton.Pressed += () => ResumeRequested?.Invoke();
        exitButton.Pressed += () => ExitRequested?.Invoke();
    }
}
