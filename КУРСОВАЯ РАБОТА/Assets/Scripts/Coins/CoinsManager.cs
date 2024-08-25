using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    private static CoinsManager _instance;
    private TMP_Text coinsAmount;
    private int currentCoins;
    private const string COIN_AMOUNT_TEXT = "CoinsAmount";

    public static CoinsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var existingObject = FindObjectOfType<CoinsManager>();
                if (existingObject != null)
                {
                    _instance = existingObject;
                }
                else
                {
                    var singletonObject = new GameObject("CoinsManager");
                    _instance = singletonObject.AddComponent<CoinsManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeCoinsDisplay();
    }

    public int CurrentCoins()
    {
        return currentCoins;
    }

    public void RemoveCoins(int amount)
    {
        currentCoins -= amount;
        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
        UpdateDisplay();
    }

    private void InitializeCoinsDisplay()
    {
        if (coinsAmount == null)
        {
            var coinsDisplayObject = GameObject.Find(COIN_AMOUNT_TEXT);
            if (coinsDisplayObject)
            {
                coinsAmount = coinsDisplayObject.GetComponent<TMP_Text>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogError("CoinsManager: Failed to find a coins display object in the scene.");
            }
        }
        UpdateDisplay();
    }

    public void AddCoins(int amount)
    {
        while (amount-- > 0)
        {
            currentCoins += 10;
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (coinsAmount != null)
        {
            coinsAmount.text = currentCoins.ToString("D4");
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("CoinsManager: TMP_Text component for displaying coins is not set.");
        }
    }
    
    public void SetCoinsText(TMP_Text textComponent)
    {
        coinsAmount = textComponent;
        UpdateDisplay();
    }
}
