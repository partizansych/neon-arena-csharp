// Состояние пушки
using System;

public class Gun {
    public event Action ReloadEnded;

    public float TimeSinceShot { get; private set; }
    public int Ammo { get; private set; }
    public float ReloadTimer { get; private set; }
    public bool IsReloading { get; private set; }

    public GunData Data { get; private set; }

    public Gun(GunData data) {
        Data = data;
        TimeSinceShot = data.FireRate;
        Ammo = data.MaxAmmo;
    }

    public void Tick(float delta) {
        if (TimeSinceShot < Data.FireRate) {
            TimeSinceShot += delta;
        }

        if (IsReloading) {
            ReloadTimer -= delta;
            if (ReloadTimer <= 0f) {
                IsReloading = false;
                Ammo = Data.MaxAmmo;
                ReloadEnded?.Invoke();
            }
        }
    }

    public bool TryDoShot() {
        if (!IsReloading && TimeSinceShot >= Data.FireRate && Ammo > 0) {
            TimeSinceShot = 0f;
            Ammo--;
            return true;
        }
        return false;
    }

    public bool TryStartReload() {
        if (!IsReloading && Ammo < Data.MaxAmmo) {
            IsReloading = true;
            ReloadTimer = Data.ReloadTime;
            return true;
        }
        return false;
    }

}
