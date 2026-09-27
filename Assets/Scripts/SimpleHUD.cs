using UnityEngine;
using UnityEngine.UI;

public class SimpleHUD : MonoBehaviour
{
    public Text ammoText;
    public Text healthText;
    public Image crosshairImg;
    public Text roundTimerText;
    public Text scoreText;
    private PlayerController player;

    private void Start() => player = FindObjectOfType<PlayerController>();

    private void Update()
    {
        if (player == null) return;
        if (healthText != null) healthText.text = $"HP {Mathf.CeilToInt(player.health)}";
        if (player.activeWeapon != null)
        {
            if (ammoText != null) ammoText.text = $"{player.activeWeapon.ammoInMagazine} / {player.activeWeapon.ammoReserve}";
            if (crosshairImg != null)
            {
                float scale = 1f + player.activeWeapon.CurrentSpread * 20f;
                crosshairImg.rectTransform.localScale = Vector3.one * scale;
            }
        }
    }
}
