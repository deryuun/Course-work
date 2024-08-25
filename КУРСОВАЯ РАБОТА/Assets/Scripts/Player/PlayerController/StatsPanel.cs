using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    public GameObject statsPanel;
    public bool isStatPanelOpen;
    
    [Header("Levels")]
    private int damageLevel;
    private int defenseLevel;
    private int speedLevel;
    
    [Header("Level text")]
    public TMP_Text damageLevelText;
    public TMP_Text defenseLevelText;
    public TMP_Text speedLevelText;
    
    [Header("Current stat")]
    public TMP_Text damageCurrentText;
    public TMP_Text defenseCurrentText;
    public TMP_Text speedCurrentText;

    [Header("Sybmols")] 
    public GameObject symbol1;
    public GameObject symbol2;
    public GameObject symbol3;
    public GameObject symbol4;

    void Start()
    {
        damageLevel = PlayerStats.Instance.DamageLevel;
        defenseLevel = PlayerStats.Instance.DefenseLevel;
        speedLevel = PlayerStats.Instance.SpeedLevel;
        
        UpdateUI();
    }
    
    void UpdateUI()
    {
        damageCurrentText.text =  $"Damage: {10 + 5 * damageLevel}";
        defenseCurrentText.text = $"Defence: {defenseLevel}%";
        speedCurrentText.text = $"Speed: {5.0f + 0.5f * (speedLevel - 1)}";

        damageLevelText.text =  $"Lvl: {damageLevel}";
        defenseLevelText.text = $"Lvl: {defenseLevel}";
        speedLevelText.text = $"Lvl: {speedLevel}";
    }
    
    public void OnCloseButtonPressed()
    {
        symbol1.SetActive(false);
        symbol2.SetActive(false);
        symbol3.SetActive(false);
        symbol4.SetActive(false);
        isStatPanelOpen = false;
        statsPanel.SetActive(false);
    }
}
