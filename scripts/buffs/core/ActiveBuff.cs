using Godot;

namespace Buffs;

public class ActiveBuff {
    public BuffData Data { get; }
    public Node Target { get; }

    public float Remaining { get; private set; }
    public float TickTimer { get; private set; }
    public int Stacks { get; private set; } = 1;

    public bool IsExpired => Data.Duration > 0f && Remaining <= 0f;

    public ActiveBuff(BuffData data, Node target) {
        Data = data;
        Target = target;
        Remaining = data.Duration;
    }

    public void Start() {
        foreach (var effect in Data.Effects) {
            effect.OnApply(Target, this);
        }
    }

    public void Update(float delta) {
        if (Data.Duration > 0f) {
            Remaining -= delta;
        }

        if (Data.IsTickable && Data.TickInterval > 0f) {
            TickTimer += delta;
            while (TickTimer >= Data.TickInterval) {
                TickTimer -= Data.TickInterval;
                Tick();
            }
        }
    }

    public void End() {
        foreach (var effect in Data.Effects) {
            effect.OnRemove(Target, this);
        }
    }

    public bool TryAddStack() {
        Remaining = Data.Duration;

        if (Stacks < Data.MaxStacks) {
            Stacks++;
            foreach (var effect in Data.Effects) {
                effect.OnStackChanged(Target, this);
            }
            return true;
        }

        return false;
    }

    private void Tick() {
        foreach (var effect in Data.Effects) {
            effect.OnTick(Target, this);
        }
    }
}
