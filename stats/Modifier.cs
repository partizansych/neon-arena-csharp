using System;

namespace NeonArenaCsharp.stats;

// Final = (Base + Flat) * (1 + Sum(Additive)) * (1 + Sum(Multiplicative))
public enum ModifierType {
    Flat,           // +10 урона
    Additive,       // +20% урона (складывается с другими %)
    Multiplicative  // +20% урона (умножается на итог)
}

public readonly struct Modifier(float value, ModifierType type, string sourceId) : IEquatable<Modifier> {
    public readonly float Value = value;
    public readonly ModifierType Type = type;
    public readonly string SourceId = sourceId;

    public bool Equals(Modifier other) {
        // Сравниваем Источник и Тип. 
        // Значение (Value) не сравниваем, так как мы можем захотеть заменить старый модификатор на новый с другим значением от того же источника.
        // Но в логике добавления мы обычно запрещаем дубликаты Источник+Тип.
        return SourceId == other.SourceId && Type == other.Type;
    }

    // Переопределяем стандартный Equals для совместимости с object
    public override bool Equals(object obj) {
        return obj is Modifier other && Equals(other);
    }

    // Обязательно для работы в HashSet, Dictionary и других коллекциях
    public override int GetHashCode() {
        // Комбинируем хеш-коды полей. 
        // В .NET Core / .NET 5+ HashCode.Combine работает отлично.
        return HashCode.Combine(SourceId, Type);
    }

    public override string ToString() {
        return $"Mod(Value: {Value}, Type: {Type}, Source: {SourceId})";
    }

    public static bool operator ==(Modifier left, Modifier right) {
        return left.Equals(right);
    }

    public static bool operator !=(Modifier left, Modifier right) {
        return !(left == right);
    }
}