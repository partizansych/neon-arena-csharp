using Godot;

public enum PlayerStat {
    Speed,
    MaxHp
}

public enum PlayerSound {
    Hit
}

[GlobalClass]
public partial class PlayerData : Resource {
    [ExportGroup("Базовые значения атрибутов")]
    [Export] public float Speed { get; private set; } = 300f;
    [Export] public float MaxHp { get; private set; } = 100f;

    [ExportGroup("")]
    [Export] Godot.Collections.Dictionary<PlayerSound, AudioStreamWav> sounds;

    public bool TryGetSound(PlayerSound type, out AudioStreamWav sound) {
        return sounds.TryGetValue(type, out sound);
    }
}
