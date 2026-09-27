using UnityEngine;
using UnityEngine.UI;

public class SimpleHUD : MonoBehaviour
{
    public Text ammoText;
    public Text healthText;
    public Image crosshairImg;
    public RectTransform minimapContainer;
    public Text roundTimerText;
    public Text scoreText;

    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (player == null) return;
        var w = player.activeWeapon;
        ammoText.text = $"{w.ammoInMagazine} / {w.ammoReserve}";
        healthText.text = $"{player.health}";
        // update crosshair size based on accuracy
        float scale = 1f + w.currentSpread * 20f;
        crosshairImg.rectTransform.localScale = Vector3.one * scale;

        // round timer & score should hook into game manager (placeholder)
    }
}