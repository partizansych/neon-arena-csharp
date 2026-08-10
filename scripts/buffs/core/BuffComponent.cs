using System.Collections.Generic;
using Godot;

namespace Buffs;

[GlobalClass]
public partial class BuffComponent : Node {
    readonly List<ActiveBuff> activeBuffs = [];

    public override void _Process(double delta) {
        for (int i = activeBuffs.Count - 1; i >= 0; i--) {
            var buff = activeBuffs[i];
            buff.Update((float)delta);

            if (buff.IsExpired) {
                buff.End();
                activeBuffs.RemoveAt(i);
            }
        }
    }

    public void Add(BuffData data) {
        var existing = activeBuffs.Find(b => b.Data.Id == data.Id);

        if (existing != null) {
            existing.TryAddStack();
            return;
        }

        var newBuff = new ActiveBuff(data, Owner);
        activeBuffs.Add(newBuff);
        newBuff.Start();
    }
}
