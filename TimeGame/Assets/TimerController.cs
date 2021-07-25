using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject startTimerUI;
    public int roundStartDelayTime;
    
    void Start()
    {
        StartCoroutine(CountDownToStart());
    }

    private IEnumerator CountDownToStart()
    {
        while (roundStartDelayTime > 0)
        {
            timerText.text = roundStartDelayTime.ToString();
            yield return new WaitForSeconds(1f);
            roundStartDelayTime--;
        }

        timerText.text = "GO!";
        yield return new WaitForSeconds(1f);
        startTimerUI.gameObject.SetActive(false);
    }
}
