using UnityEngine;
using UnityEngine.Serialization;

public class InteractionObject : MonoBehaviour
{
    public UpgradeManager upManager;
    public StatsPanel statsView;
    
    public GameObject upgradePanel;
    public GameObject statsPanel;
    public GameObject button;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(false);
        }
    }

    public void OnOpenButtonPressed()
    {
        if (upManager != null && statsView == null)
        {
            upgradePanel.SetActive(true);
            upManager.isUpgradePanelOpen = true;
            button.SetActive(false);
        }
        else if(upManager == null && statsView != null)
        {
            statsView.symbol1.SetActive(true);
            statsView.symbol2.SetActive(true);
            statsView.symbol3.SetActive(true);
            statsView.symbol4.SetActive(true);
            statsPanel.SetActive(true);
            statsView.isStatPanelOpen = true;
            button.SetActive(false);
        }
    }
}