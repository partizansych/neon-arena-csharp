using Godot;

[GlobalClass]
public partial class DashGhost : Node2D {
    [Export] DashMovementModifier dash;
    [Export] Sprite2D sprite;
    [Export] public float SpawnInterval = 0.05f;

    float spawnTimer;

    public override void _Ready() {
        if (dash == null) {
            GD.PushError($"[{Name}]: Ссылка на DashComponent не установлена.");
            return;
        }

        if (sprite == null) {
            GD.PushError($"[{Name}]: Ссылка на Sprite2D не установлена.");
            return;
        }
    }

    public override void _PhysicsProcess(double delta) {
        if (!dash.IsDashing) return;

        spawnTimer -= (float)delta;
        if (spawnTimer <= 0f) {
            SpawnGhost();
            spawnTimer = SpawnInterval;
        }
    }

    // спросить у ИИ, если спрайта нету, выдавать ошибку или return,
    // с учётом, что я уже проверил в Ready
    private void SpawnGhost() {
        var ghost = new Sprite2D {
            GlobalPosition = GlobalPosition,
            Texture = sprite.Texture
        };
        GetTree().CurrentScene.AddChild(ghost);

        var tween = ghost.CreateTween();
        tween.TweenProperty(ghost, "modulate:a", 0f, 0.3f);
        tween.TweenCallback(Callable.From(ghost.QueueFree));
    }
}
