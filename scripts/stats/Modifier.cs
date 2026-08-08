namespace Stats;

// Final = ((Base + Sum(Flat)) * (1 + Sum(PercentAdd))) * MulAll(1 + PercentMult)
public enum ModifierType {
    Flat,        // +10 урона
    PercentAdd,  // +25% урона (0.25f, складывается с другими PercentAdd)
    PercentMult  // +15% урона (0.15f, перемножается с итоговым значением)
}

// Использование object в struct приводит к boxing,
// но, только если передавать структурный тип (int, enum)
public readonly record struct Modifier(float Value, ModifierType Type, object Source = null);
