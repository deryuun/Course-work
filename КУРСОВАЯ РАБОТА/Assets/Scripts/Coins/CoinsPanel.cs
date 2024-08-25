using TMPro;
using UnityEngine;

public class CoinsPanel : MonoBehaviour
{
    public TMP_Text coinsText;

    private void Start()
    {
        if (CoinsManager.Instance != null)
        {
            coinsText = GetComponentInChildren<TMP_Text>();
            if (coinsText != null)
            {
                CoinsManager.Instance.SetCoinsText(coinsText);
            }
        }
    }
}