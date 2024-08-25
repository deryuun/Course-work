using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveAnnouncer : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public float announcementTime = 2.0f; 

    void Start()
    {
        waveText.gameObject.SetActive(false); 
    }

    public void AnnounceWave(int waveNumber)
    {
        if (waveNumber == 10)
        {
            AnnounceBoss();
        }
        waveText.text = "Wave " + waveNumber;
        StartCoroutine(ShowAndHideText());
    }

    public void AnnounceBoss()
    {
        waveText.text = "Boss AUB is HERE";
        StartCoroutine(ShowAndHideText());
    }

    private IEnumerator ShowAndHideText()
    {
        waveText.gameObject.SetActive(true);
        waveText.CrossFadeAlpha(1.0f, 0.5f, false); 
        yield return new WaitForSeconds(announcementTime);
        waveText.CrossFadeAlpha(0.0f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        waveText.gameObject.SetActive(false);
    }
}
