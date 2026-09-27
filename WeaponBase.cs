using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Stats")]
    public string weaponName = "Prototype";
    public float damage = 25f;
    public float headshotMultiplier = 2.0f;
    public int magazineSize = 30;
    public int ammoInMagazine = 30;
    public int ammoReserve = 90;
    public float fireRateRPM = 600f; // rounds per minute
    public bool isAutomatic = true;
    public float reloadTime = 2.0f;
    public float range = 100f;
    public float spread = 0.02f; // radians
    public float recoilPerShot = 0.1f;
    public float spreadRecoveryPerSecond = 1.0f;

    protected float lastFireTime = 0f;
    protected float currentSpread = 0f;
    protected bool isReloading = false;

    protected virtual void Update()
    {
        // spread recovery
        currentSpread = Mathf.Max(0f, currentSpread - spreadRecoveryPerSecond * Time.deltaTime);

        if (isReloading) return;
        // input handled externally
    }

    public bool CanFire()
    {
        if (isReloading) return false;
        if (ammoInMagazine <= 0) return false;
        float interval = 60f / fireRateRPM;
        return Time.time >= lastFireTime + interval;
    }

    public virtual void StartReload()
    {
        if (isReloading || ammoInMagazine >= magazineSize || ammoReserve <= 0) return;
        isReloading = true;
        Invoke(nameof(FinishReload), reloadTime);
    }

    protected virtual void FinishReload()
    {
        int needed = magazineSize - ammoInMagazine;
        int taken = Mathf.Min(needed, ammoReserve);
        ammoInMagazine += taken;
        ammoReserve -= taken;
        isReloading = false;
    }

    protected void ConsumeAmmo()
    {
        ammoInMagazine = Mathf.Max(0, ammoInMagazine - 1);
    }
}