using NeonArenaCsharp;

public class CharacterStatSheet : StatSheet<StatType> {
    public CharacterStatSheet(CharacterData data) {
        Register(StatType.Speed, data.Speed);
        Register(StatType.MaxHp, data.MaxHp);
    }
}
