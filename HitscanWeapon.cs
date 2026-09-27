using UnityEngine;

public class HitscanWeapon : WeaponBase
{
    public Transform muzzlePoint;
    public LayerMask hitMask;
    public GameObject hitEffectPrefab;

    public void TryFire(Vector3 aimDirection)
    {
        if (!CanFire()) return;

        lastFireTime = Time.time;
        ConsumeAmmo();
        currentSpread += spread;

        // apply recoil (this is placeholder; integrate with camera/controller)
        ApplyRecoil();

        // compute final direction with spread
        Vector3 dir = ApplySpreadToDirection(aimDirection, currentSpread);

        if (Physics.Raycast(muzzlePoint.position, dir, out RaycastHit hit, range, hitMask))
        {
            // assume target has Health component
            var health = hit.collider.GetComponent<Health>();
            float appliedDamage = damage;
            if (IsHeadshot(hit)) appliedDamage *= headshotMultiplier;
            if (health != null) health.ApplyDamage(appliedDamage);

            if (hitEffectPrefab)
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    bool IsHeadshot(RaycastHit hit)
    {
        // Very simple headshot detection by tag or layer
        return hit.collider.CompareTag("Head");
    }

    Vector3 ApplySpreadToDirection(Vector3 baseDir, float spreadAmount)
    {
        // convert spread (radians) into random cone
        float angle = spreadAmount;
        return Vector3.RotateTowards(baseDir, Random.onUnitSphere, angle, 0f);
    }

    void ApplyRecoil()
    {
        // Placeholder: signal to player controller to modify view
        // e.g., playerCamera.transform.Rotate(-recoilPerShot, Random.Range(-recoilPerShot, recoilPerShot), 0);
    }
}