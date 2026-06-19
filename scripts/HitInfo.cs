using Godot;

public enum HitType {
    Damage,
    Heal,
}

public enum HitElement {
    Physical, // либо None
    Fire,
}

// Может сильно расширяться (если будут новые механики).
// Это типа гибко и расширяемо.
// Если будет сильно большим - сделать классом.
// Можно будет сделать пул таким классов.
//
// Если нужно будет хранить ссылку на оружие, то нужно делать исходя из архитектуры.
// Часто требуются всего лишь статы оружия, можно их передавать.
public struct HitInfo {
    public Node2D Source;
    public Vector2 HitPoint;

    public HitType Type;
    public HitElement Element;
    public float Value;

    public float KnockbackForce;
}
