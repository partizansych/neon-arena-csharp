// Состояние пушки
using System;

public class Gun {
    public event Action ReloadEnded;

    float timeSinceShot;
    int ammo;
    float reloadTimer;
    bool isReloading;

    public GunData Data { get; private set; }

    public Gun(GunData data) {
        Data = data;
        timeSinceShot = data.FireRate;
        ammo = data.MaxAmmo;
    }

    public void Tick(float delta) {
        if (timeSinceShot < Data.FireRate) {
            timeSinceShot += delta;
        }

        if (isReloading) {
            reloadTimer -= delta;
            if (reloadTimer <= 0f) {
                isReloading = false;
                ammo = Data.MaxAmmo;
                ReloadEnded?.Invoke();
            }
        }
    }

    public bool TryDoShot() {
        if (!isReloading && timeSinceShot >= Data.FireRate && ammo > 0) {
            timeSinceShot = 0f;
            ammo--;
            return true;
        }
        return false;
    }

    public bool TryStartReload() {
        if (!isReloading && ammo < Data.MaxAmmo) {
            isReloading = true;
            reloadTimer = Data.ReloadTime;
            return true;
        }
        return false;
    }

}
