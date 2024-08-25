using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public bool isUpgradePanelOpen = false;
    
    [Header("Levels")]
    public int damageLevel = 1;
    public int defenseLevel = 1;
    public int speedLevel = 1;

    [Header("Price")]
    public TMP_Text damagePriceText;
    public TMP_Text defensePriceText;
    public TMP_Text speedPriceText;

    [Header("Level text")]
    public TMP_Text damageLevelText;
    public TMP_Text defenseLevelText;
    public TMP_Text speedLevelText;
    
    [Header("Current stat")]
    public TMP_Text damageCurrentText;
    public TMP_Text defenseCurrentText;
    public TMP_Text speedCurrentText;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        damagePriceText.text = GetUpgradeCost(damageLevel).ToString();
        defensePriceText.text = GetUpgradeCost(defenseLevel).ToString();
        speedPriceText.text = GetUpgradeCost(speedLevel).ToString();
        
        damageCurrentText.text =  $"Damage: {10 + 5 * damageLevel}";
        defenseCurrentText.text = $"Defence: {defenseLevel}%";
        speedCurrentText.text = $"Speed: {5.0f + 0.5f * speedLevel}";

        damageLevelText.text =  $"Lvl: {damageLevel + 1}";
        defenseLevelText.text = $"Lvl: {defenseLevel + 1}";
        speedLevelText.text = $"Lvl: {speedLevel + 1}";
    }

    public void UpgradeDamage()
    {
        int cost = GetUpgradeCost(PlayerStats.Instance.DamageLevel);
        if (CoinsManager.Instance.CurrentCoins() >= cost)
        {
            damageLevel++;
            CoinsManager.Instance.RemoveCoins(cost);
            PlayerStats.Instance.IncreaseDamage(5); 
            UpdateUI();
        }
    }

    public void UpgradeDefense()
    {
        int cost = GetUpgradeCost(PlayerStats.Instance.DefenseLevel);
        if (CoinsManager.Instance.CurrentCoins() >= cost)
        {
            defenseLevel++;
            CoinsManager.Instance.RemoveCoins(cost);
            PlayerStats.Instance.IncreaseDefense(1); 
            UpdateUI();
        }
    }

    public void UpgradeSpeed()
    {
        int cost = GetUpgradeCost(PlayerStats.Instance.SpeedLevel);
        if (CoinsManager.Instance.CurrentCoins() >= cost)
        {
            speedLevel++;
            CoinsManager.Instance.RemoveCoins(cost);
            PlayerStats.Instance.IncreaseSpeed(0.5f); 
            UpdateUI();
        }
    }

    public void OnCloseButtonPressed()
    {
        isUpgradePanelOpen = false;
        upgradePanel.SetActive(false);
    }

    private int GetUpgradeCost(int level)
    {
        return  50 * (level);
    }
}