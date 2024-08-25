using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cooldown;
    [HideInInspector] public bool isCooldown;

    private Image shield;
    public PlayerHealth player;

    void Start()
    {
        shield = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        isCooldown = true;
    }

    void Update()
    {
        if (isCooldown)
        {
            shield.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (shield.fillAmount <= 0)
            {
                shield.fillAmount = 1;
                isCooldown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        shield.fillAmount = 1;
    }
}
