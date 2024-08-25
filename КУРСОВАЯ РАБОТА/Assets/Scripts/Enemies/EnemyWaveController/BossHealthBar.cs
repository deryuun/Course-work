using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public GameObject healthBarUI;
    public Image slimeHealthBar;
    public Image demonHealthBar;
    
    public float maxSlimeHealth = 100f;
    public float maxDemonHealth = 500f;
    public void ActivateHealthBar()
    {
        healthBarUI.SetActive(true);
        slimeHealthBar.fillAmount = 1.0f;
        demonHealthBar.fillAmount = 0f;
        demonHealthBar.gameObject.SetActive(false);
    }
    
    public void DeactivateHealthBar()
    {
        healthBarUI.SetActive(false);
    }
    
    public float slimeHp {
        set
        {
            slimeHealthBar.fillAmount = value / maxSlimeHealth;
        }
    }

    public float demonHp {
        set { demonHealthBar.fillAmount = value / maxDemonHealth; }
    }
    public void ActivateDemonHealthBar()
    {
        slimeHealthBar.gameObject.SetActive(false);
        demonHealthBar.gameObject.SetActive(true);
        demonHealthBar.fillAmount = 1.0f;
    }
}