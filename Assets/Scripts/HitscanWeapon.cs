using UnityEngine;

public class HitscanWeapon : WeaponBase
{
    public Transform muzzlePoint;
    public LayerMask hitMask = ~0;
    public GameObject hitEffectPrefab;

    public bool TryFire(Vector3 aimDirection)
    {
        if (!CanFire()) return false;
        lastFireTime = Time.time;
        ConsumeAmmo();
        currentSpread = Mathf.Min(spread * 5f, currentSpread + spread);

        Vector3 dir = ApplySpreadToDirection(aimDirection.normalized, currentSpread);
        Vector3 origin = muzzlePoint != null ? muzzlePoint.position : transform.position;

        if (Physics.Raycast(origin, dir, out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Ignore))
        {
            Health health = hit.collider.GetComponentInParent<Health>();
            float finalDamage = IsHeadshot(hit) ? damage * headshotMultiplier : damage;
            if (health != null) health.ApplyDamage(finalDamage);
            if (hitEffectPrefab != null)
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
        return true;
    }

    private bool IsHeadshot(RaycastHit hit) => hit.collider.CompareTag("Head");

    private Vector3 ApplySpreadToDirection(Vector3 baseDir, float spreadAmount)
    {
        if (spreadAmount <= 0f) return baseDir;
        Vector2 offset = Random.insideUnitCircle * spreadAmount;
        Vector3 right = Vector3.Cross(baseDir, Vector3.up);
        if (right.sqrMagnitude < 0.001f) right = Vector3.right;
        right.Normalize();
        Vector3 up = Vector3.Cross(right, baseDir).normalized;
        return (baseDir + right * offset.x + up * offset.y).normalized;
    }
}
