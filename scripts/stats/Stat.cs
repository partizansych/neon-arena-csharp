using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stats;

public class Stat {
    public event Action ValueChanged;

    private float baseValue;
    private float cachedValue;
    private bool isDirty = true;

    private readonly List<Modifier> modifiers = [];

    public float BaseValue {
        get => baseValue;
        set {
            if (baseValue != value) {
                baseValue = value;
                OnStatChanged();
            }
        }
    }

    public float Value {
        get {
            if (isDirty) {
                cachedValue = Calculate();
                isDirty = false;
            }
            return cachedValue;
        }
    }

    public Stat(float baseValue) {
        this.baseValue = baseValue;
    }

    public void Add(Modifier mod) {
        modifiers.Add(mod);
        OnStatChanged();
    }

    public void Add(IEnumerable<Modifier> mods) {
        ArgumentNullException.ThrowIfNull(mods);

        int previousCount = modifiers.Count;
        modifiers.AddRange(mods);

        if (modifiers.Count > previousCount) {
            OnStatChanged();
        }
    }

    public bool Remove(Modifier mod) {
        if (modifiers.Remove(mod)) {
            OnStatChanged();
            return true;
        }
        return false;
    }

    public int Remove(IEnumerable<Modifier> mods) {
        ArgumentNullException.ThrowIfNull(mods);

        int removedCount = modifiers.RemoveAll(mods.Contains);
        if (removedCount > 0) {
            OnStatChanged();
        }

        return removedCount;
    }

    public int Remove(object source) {
        if (source == null) return 0;

        int removed = modifiers.RemoveAll(m => ReferenceEquals(m.Source, source));
        if (removed > 0) {
            OnStatChanged();
        }
        return removed;
    }

    // Вызывается в конце любой операции,
    // которая меняет итоговое значение.
    private void OnStatChanged() {
        isDirty = true;
        ValueChanged?.Invoke();
    }

    private float Calculate() {
        float flatSum = 0f;
        float additiveSum = 0f;
        float multiplicativeMult = 1f;

        for (int i = 0; i < modifiers.Count; i++) {
            var mod = modifiers[i];
            switch (mod.Type) {
                case ModifierType.Flat:
                    flatSum += mod.Value;
                    break;
                case ModifierType.PercentAdd:
                    additiveSum += mod.Value;
                    break;
                case ModifierType.PercentMult:
                    multiplicativeMult *= 1f + mod.Value;
                    break;
            }
        }

        return (baseValue + flatSum) * (1f + additiveSum) * multiplicativeMult;
    }

    public override string ToString() {
        return $"{Value:F1} (Base: {BaseValue:F1}, Mods: {modifiers.Count})";
    }

    public string GetDetailedString() {
        if (modifiers.Count == 0) {
            return $"Stat Value: {Value:F2} (Base: {BaseValue:F2}, No Modifiers)";
        }

        var sb = new StringBuilder();
        sb.AppendLine($"Stat Value: {Value:F2} (Base: {BaseValue:F2})");
        sb.AppendLine("Modifiers:");

        for (int i = 0; i < modifiers.Count; i++) {
            var mod = modifiers[i];
            string sourceInfo = mod.Source != null ? $" from {mod.Source}" : string.Empty;
            sb.AppendLine($"  [{i + 1}] {mod.Type}: {mod.Value:+0.##;-0.##;0}{sourceInfo}");
        }

        return sb.ToString().TrimEnd();
    }
}
