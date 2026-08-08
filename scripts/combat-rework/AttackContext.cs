using System.Collections.Generic;
using Godot;

// Снимок атаки в момент её начинания,
// а также черновик для последующей модификации.
public class AttackContext {
    public Node2D Source { get; init; }
    public Vector2 StartPosition { get; init; }
    public Vector2 StartDirection { get; init; }

    // Первоначально имеют значения оружия, но моды могут изменять их
    // В современных играх популярны механики вроде:
    // «Каждая 3-я атака наносит +50% урона» или
    // «При прохождении через огненный щит снаряд ускоряется».
    public float Power;
    public float Speed;
    public float Duration;
    public int MaxTargets = 1;

    // Когда снаряд врезается во врага, ему не нужно знать,
    // что именно он должен сделать.
    // Он просто берет список Impacts из своего AttackContext
    // и применяет каждый эффект к цели.
    public IReadOnlyList<WeaponImpact> Impacts { get; init; }

    public bool IsCrit;
}