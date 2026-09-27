using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0f;

    private void Awake() => CurrentHealth = maxHealth;

    public void ApplyDamage(float amount)
    {
        if (IsDead || amount <= 0f) return;
        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
        if (IsDead) OnDeath();
    }

    private void OnDeath()
    {
        if (CompareTag("Player"))
        {
            var controller = GetComponent<PlayerController>();
            if (controller != null) controller.enabled = false;
        }
    }
}
