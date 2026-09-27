using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Stats")]
    public string weaponName = "Prototype";
    public float damage = 25f;
    public float headshotMultiplier = 2f;
    public int magazineSize = 30;
    public int ammoInMagazine = 30;
    public int ammoReserve = 90;
    public float fireRateRPM = 600f;
    public bool isAutomatic = true;
    public float reloadTime = 2f;
    public float range = 100f;
    public float spread = 0.02f;
    public float recoilPerShot = 0.1f;
    public float spreadRecoveryPerSecond = 1f;

    protected float lastFireTime = -999f;
    protected float currentSpread;
    protected bool isReloading;
    public float CurrentSpread => currentSpread;
    public bool IsReloading => isReloading;

    protected virtual void Update()
    {
        currentSpread = Mathf.Max(0f, currentSpread - spreadRecoveryPerSecond * Time.deltaTime);
    }

    public bool CanFire()
    {
        if (isReloading || ammoInMagazine <= 0) return false;
        return Time.time >= lastFireTime + 60f / Mathf.Max(1f, fireRateRPM);
    }

    public virtual void StartReload()
    {
        if (isReloading || ammoInMagazine >= magazineSize || ammoReserve <= 0) return;
        isReloading = true;
        CancelInvoke(nameof(FinishReload));
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

    protected void ConsumeAmmo() => ammoInMagazine = Mathf.Max(0, ammoInMagazine - 1);
}
