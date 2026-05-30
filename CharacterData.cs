using Godot;

public enum CharacterStat {
    Speed,
    MaxHp
}

public enum CharacterSFX {
    Hit
}

[GlobalClass]
public partial class CharacterData : Resource {
    [Export] public float Speed = 300f;
    [Export] public float MaxHp = 100f;

    [Export] public AudioStream HitSFX;
}
