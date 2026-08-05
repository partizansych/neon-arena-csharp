using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class LoadingCurtain : Control {
    [Export] public float FadeDuration { get; set; } = 0.5f;

    bool isFading;

    public override void _Ready() {
        Visible = false;

        var color = Modulate;
        color.A = 0f;
        Modulate = color;
    }

    public async Task FadeInAsync() {
        if (isFading) return;

        isFading = true;
        Visible = true;

        var tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 1.0f, FadeDuration);
        await ToSignal(tween, Tween.SignalName.Finished);

        isFading = false;
    }

    public async Task FadeOutAsync() {
        if (isFading) return;

        isFading = true;

        var tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0.0f, FadeDuration);
        await ToSignal(tween, Tween.SignalName.Finished);

        Visible = false;
        isFading = false;
    }
}
