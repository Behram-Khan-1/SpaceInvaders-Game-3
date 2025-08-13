using UnityEngine;

public class BossShield : MonoBehaviour
{
    public GameObject shield; // Reference to the shield prefab
    public float shieldDuration = 5f; // Duration of the shield
    private float shieldCooldown = 5f;
    private float cooldownTimer = 0f;
    private bool shieldActive = false;

    void Start()
    {
        shield.SetActive(false); // Initially hide the shield
    }
    

    void Update()
    {
        if (Boss.currentPhase != BossPhase.Phase5)
            return; // Only activate shield in Phase 5
            
        if (shieldActive)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration <= 0)
            {
                DeactivateShield();
            }
        }

        if (!shieldActive)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                ActivateShield();
            }
        }
    }
    private void ActivateShield()
    {
        shieldActive = true;
        shieldDuration = 5f; // Reset the duration of the shield
        cooldownTimer = shieldCooldown; // Start the cooldown timer

        shield.SetActive(true);
    }

    private void DeactivateShield()
    {
        shieldActive = false;
        shield.SetActive(false);
    }
}