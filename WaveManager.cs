using Godot;

[GlobalClass]
public partial class WaveManager : Node {
    [Export] public PackedScene EnemyScene;
    [Export] public int EnemiesPerWave = 5;
    [Export] public float SpawnDelay = 1f;

    public Node2D Player;

    private int enemiesAlive;
    private int currentWave = 1;
    private bool isSpawning;

    public void StartWave() {
        if (isSpawning) return;

        isSpawning = true;
        GD.Print($"--- Starting Wave {currentWave} ---");

        for (int i = 0; i < EnemiesPerWave + (currentWave * 2); i++) {
            SpawnEnemy();
        }

        isSpawning = false;
    }

    private void SpawnEnemy() {
        var enemy = EnemyScene.Instantiate<Protostar>();
        enemy.GlobalPosition = GetRandomPos(Vector2.Zero);
        enemy.Target = Player;
        AddChild(enemy);

        enemiesAlive++;
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied() {
        enemiesAlive--;
        GD.Print($"Enemies left: {enemiesAlive}");

        if (enemiesAlive <= 0 && !isSpawning) {
            GD.Print("Wave Complete!");
            currentWave++;
            GetTree().CreateTimer(2.0f).Timeout += StartWave;
        }
    }

    private Vector2 GetRandomPos(Vector2 origin) {
        var viewportHalf = GetViewport().GetVisibleRect().Size / 2;
        return new Vector2(
            (float)GD.RandRange(origin.X - viewportHalf.X, origin.X + viewportHalf.X),
            (float)GD.RandRange(origin.Y - viewportHalf.Y, origin.Y + viewportHalf.Y)
        );
    }
}
