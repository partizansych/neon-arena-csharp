using NeonArenaCsharp;

public class WeaponStatSheet : StatSheet<AttributeType> {
    public WeaponStatSheet(WeaponData data) {
        Register(AttributeType.Damage, data.Damage);
        Register(AttributeType.FireRate, data.FireRate);
        Register(AttributeType.ReloadTime, data.ReloadTime);
        Register(AttributeType.BulletSpeed, data.BulletSpeed);
        Register(AttributeType.BulletLifetime, data.BulletLifetime);
        Register(AttributeType.MaxAmmo, data.MaxAmmo);
    }
}
