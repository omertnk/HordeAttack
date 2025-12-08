using UnityEngine;
using TMPro; // Needed for UI Text

public class AbilityUnlockManager : MonoBehaviour
{
    [Header("Coin System")]
    public int currentCoins = 0;
    public TextMeshProUGUI coinText; // Drag your UI Text here later

    [Header("Abilities (Drag Scripts Here)")]
    public MonoBehaviour defaultAbility;   // e.g. Ghostly Slash (if you have it)
    public DaggerLauncher daggerAbility;   // Your Dagger Script
    public SkyfireAbility skyfireAbility;  // Your Skyfire Script
    public ArcaneLauncher arcaneAbility;   // Your Arcane Orb Script

    [Header("Unlock Costs")]
    public int daggerCost = 100;
    public int skyfireCost = 200; // 100 + 100
    public int arcaneCost = 300;  // 100 + 100 + 100

    void Start()
    {
        UpdateAbilities(); // Set initial state
        UpdateUI();
    }

    // Call this function whenever you pick up a coin
    public void AddCoin(int amount)
    {
        currentCoins += amount;
        UpdateUI();
        UpdateAbilities();
    }

    void UpdateAbilities()
    {
        // 1. Default Ability (Always ON until replaced? Or always ON?)
        // The GDD says "switches to", implying the old one might stop, 
        // OR they might stack. Let's assume they STACK (player gets stronger).
        // If you want them to SWAP (only 1 active), change 'true' to 'false' in the else blocks.

        // Level 1: Dagger Volley (100 Coins)
        if (currentCoins >= daggerCost)
        {
            if (daggerAbility != null) daggerAbility.enabled = true;
        }
        else
        {
            if (daggerAbility != null) daggerAbility.enabled = false;
        }

        // Level 2: Skyfire (200 Coins)
        if (currentCoins >= skyfireCost)
        {
            if (skyfireAbility != null) skyfireAbility.enabled = true;
        }
        else
        {
            if (skyfireAbility != null) skyfireAbility.enabled = false;
        }

        // Level 3: Arcane Orb (300 Coins)
        if (currentCoins >= arcaneCost)
        {
            if (arcaneAbility != null) arcaneAbility.enabled = true;
        }
        else
        {
            if (arcaneAbility != null) arcaneAbility.enabled = false;
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + currentCoins;
        }
    }
}