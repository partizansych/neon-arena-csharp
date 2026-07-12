using Godot;

[GlobalClass]
public abstract partial class WeaponModuleData : Resource {
    public abstract IWeaponModule CreateRuntimeModule();
}
