#nullable enable
using System;
using System.Collections.Generic;
using Godot;
using NeonArenaCsharp;

[GlobalClass]
public partial class Loadout : Node {
    public enum Slot {
        Primary,
        Heavy
    }

    [Export] WeaponData? initialPrimaryData = null;
    [Export] WeaponData? initialHeavyData = null;

    public event Action<Slot, Weapon>? Equipped;
    public event Action<Slot, Slot>? Switched;
    public event Action<Slot, Weapon>? Replaced;

    readonly Dictionary<Slot, Weapon?> slots = [];
    Slot current;

    public override void _Ready() {
        if (initialPrimaryData != null) Equip(Slot.Primary, initialPrimaryData);
        if (initialHeavyData != null) Equip(Slot.Heavy, initialHeavyData);
    }

    public override void _Process(double delta) {
        foreach (var weapon in slots.Values) {
            weapon?.Tick((float)delta);
        }
    }

    public void Equip(Slot slot, WeaponData data) {
        if (slots.TryGetValue(slot, out var existing)) {
            if (existing != null) {
                Replaced?.Invoke(slot, existing);
            }
        }
        var weapon = new Weapon(data);
        slots[slot] = weapon;
        Equipped?.Invoke(slot, weapon);
    }

    public void SwitchTo(Slot slot) {
        if (current != slot) {
            var oldSlot = current;
            current = slot;
            Switched?.Invoke(oldSlot, slot);
        }
    }

    public Weapon? GetCurrent() {
        if (slots.TryGetValue(current, out var weapon))
            return weapon;
        return null;
    }
}
#nullable disable
