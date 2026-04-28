using System;
using Godot;

public partial class WaveManager : Node
{
    [Export] public PackedScene EnemyScene;
    [Export] public int EnemiesPerWave = 5;
    [Export] public float SpawnDelay = 1f;

    private int enemiesAlive;
    private int currentWave = 1;
    private bool isSpawning;

    public override void _Ready()
    {
        StartWave();
    }

    public async void StartWave()
    {
        if (isSpawning) return;

        isSpawning = true;
        GD.Print($"--- Starting Wave {currentWave} ---");

        for (int i = 0; i < EnemiesPerWave + (currentWave * 2); i++)
        {
            SpawnEnemy();
            await ToSignal(GetTree().CreateTimer(SpawnDelay), "timeout");
        }

        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        var enemy = EnemyScene.Instantiate<Enemy>();
        enemy.GlobalPosition = GetRandomPos();
        AddChild(enemy);

        enemiesAlive++;
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        enemiesAlive--;
        GD.Print($"Enemies left: {enemiesAlive}");

        if (enemiesAlive <= 0 && !isSpawning)
        {
            GD.Print("Wave Complete!");
            currentWave++;
            GetTree().CreateTimer(2.0f).Timeout += StartWave;
        }
    }

    private Vector2 GetRandomPos()
    {
        var viewportSize = GetViewport().GetVisibleRect().Size;
        return new Vector2(
            (float)GD.RandRange(50f, viewportSize.X - 50f),
            (float)GD.RandRange(50f, viewportSize.Y - 50f)
        );
    }
}
