using System;

namespace NeonArenaCsharp;

public class Weapon {
    public event Action Shot;
    public event Action ReloadStarted;
    public event Action ReloadEnded;

    readonly WeaponData data;
    readonly StatSheet<AttributeType> attributes;

    float timeSinceShot;
    int ammo;
    float reloadTimer;
    bool isReloading;

    public Weapon(WeaponData data) {
        this.data = data;
        attributes = new WeaponStatSheet(data);
        timeSinceShot = data.FireRate;
        ammo = data.MaxAmmo;
    }

    public float Get(AttributeType type) {
        return attributes.GetValue(type);
    }

    public WeaponData GetData() {
        return data;
    }

    public void Tick(float delta) {
        timeSinceShot += delta;

        if (isReloading) {
            reloadTimer -= delta;
            if (reloadTimer <= 0f) {
                isReloading = false;
                ammo = (int)Get(AttributeType.MaxAmmo);
                ReloadEnded?.Invoke();
            }
        }
    }

    public void DoShot() {
        if (!isReloading && timeSinceShot >= Get(AttributeType.FireRate) && ammo > 0) {
            timeSinceShot = 0f;
            ammo--;
            Shot?.Invoke();
        }
    }

    public void StartReload() {
        if (!isReloading) {
            isReloading = true;
            reloadTimer = Get(AttributeType.ReloadTime);
            ReloadStarted?.Invoke();
        }
    }
}
