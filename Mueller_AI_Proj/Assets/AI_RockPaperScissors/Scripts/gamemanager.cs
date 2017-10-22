using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour {

    public static int trialNum = 0;
    public int timeValue = 15;
    public Text timerText;
    public Text trialText;
    public Text PlayerWinText;
    public Text AIWinText;


    public void OnStartClick()
    {
        trialNum++;
        timeValue = 15;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        timeValue--;
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        trialText.text = "Trial #" + trialNum.ToString();
        timerText.text = timeValue.ToString();
        if(timeValue <= 0)
        {
            StopAllCoroutines();
        }
    }
}
