using Godot;

namespace NeonArenaCsharp;

[GlobalClass]
public partial class Character : CharacterBody2D, IDamageable {
    [Signal] public delegate void DiedEventHandler();

    [Export] CharacterData initialData;

    [ExportGroup("Компоненты")]
    [Export] Health Health;

    [ExportGroup("Звуки")]
    [Export] AudioStreamWav HitSound;
    [Export] AudioStreamWav DeathSound;

    StatSheet<StatType> stats;

    public override void _Ready() {
        Health.Died += OnDied;
        if (initialData != null) Setup(initialData);
    }

    public void Setup(CharacterData data) {
        stats = new CharacterStatSheet(data);
    }

    public float Get(StatType type) {
        return stats.GetValue(type);
    }

    public void TakeDamage(float amount) {
        Health.Reduce(amount);
        Audio.Instance.Play(HitSound, Audio.BUS_SFX);
    }

    private void OnDied() {
        Audio.Instance.Play(DeathSound, Audio.BUS_SFX);
        EmitSignal(SignalName.Died);
        QueueFree();
    }
}
