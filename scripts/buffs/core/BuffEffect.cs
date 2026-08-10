using Godot;

namespace Buffs;

[GlobalClass]
public abstract partial class BuffEffect : Resource {
    // Вызывается при наложении баффа
    public virtual void OnApply(Node target, ActiveBuff buff) { }

    // Вызывается на каждом тике (если бафф периодический)
    public virtual void OnTick(Node target, ActiveBuff buff) { }

    // Вызывается при изменении количества стаков
    public virtual void OnStackChanged(Node target, ActiveBuff buff) { }

    // Вызывается при спадении/снятии баффа
    public virtual void OnRemove(Node target, ActiveBuff buff) { }
}
