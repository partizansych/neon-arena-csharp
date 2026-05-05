using Godot;

namespace NeonArenaCsharp;

public struct Request {
    public Character Source;
    public Character Target;
    public float Damage;
}

public partial class Combat : Node {
    public void Request(Request request) {
        request.Target.TakeDamage(request.Damage);
    }
}
