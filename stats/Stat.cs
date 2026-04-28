using System.Collections.Generic;

namespace NeonArenaCsharp.stats;

public class Stat(float baseValue) {
    float baseValue = baseValue;
    float cachedResult;
    bool isDirty = true;

    readonly List<Modifier> mods = [];

    public float Value {
        get {
            if (isDirty) Recalculate();
            return cachedResult;
        }
    }

    public float BaseValue {
        get => baseValue;
        set {
            if (baseValue != value) {
                baseValue = value;
                isDirty = true;
            }
        }
    }

    public bool AddModifier(Modifier modifier) {
        // Проверка на дубликаты от одного источника одного типа
        // (Например, нельзя иметь два плоских бонуса от одного меча)
        foreach (var mod in mods)
            if (mod.Source == modifier.Source && mod.Type == modifier.Type)
                return false;

        mods.Add(modifier);
        isDirty = true;
        return true;
    }

    public void RemoveModifiersBySource(object source) {
        int removedCount = mods.RemoveAll(m => m.Source == source);
        if (removedCount > 0) isDirty = true;
    }

    private void Recalculate() {
        float flatSum = 0f;
        float additiveSum = 0f;
        float multiplicativeMult = 1f;

        foreach (var mod in mods) {
            switch (mod.Type) {
                case ModifierType.Flat:
                    flatSum += mod.Value;
                    break;
                case ModifierType.Additive:
                    additiveSum += mod.Value;
                    break;
                case ModifierType.Multiplicative:
                    multiplicativeMult *= 1f + mod.Value;
                    break;
            }
        }

        // Формула: (База + Плоские) * (1 + Сумма Аддитивных) * (Мультипликативный множитель)
        cachedResult = (baseValue + flatSum) * (1f + additiveSum) * multiplicativeMult;
        isDirty = false;
    }

    public override string ToString() {
        return $"Stat(Base: {baseValue}, Current: {cachedResult}, Mods: {mods.Count})";
    }
}