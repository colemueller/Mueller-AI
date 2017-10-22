using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour {


    public enum Type { ROCK, PAPER, SCISSORS};
    public Type playerChoice;
    public Type AIChoice;

    public int rockFreq = 0;
    public int paperFreq = 0;
    public int scissorFreq = 0;

    public static int trialNum = 0;
    public int timeValue = 15;
    public GameObject btnGroup;
    public Text timerText;
    public Text trialText;
    public Text PlayerWinText;
    public Text AIWinText;
    public Text PlayerSelectionText;
    public Text AISelectionText;


    public void OnStartClick()
    {
        btnGroup.SetActive(true);
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
        if (timeValue <= 0)
        {
            btnGroup.SetActive(false);
            StopAllCoroutines();
            MakeAIChoice();
        }

        switch (playerChoice)
        {
            case Type.ROCK:
                PlayerSelectionText.text = "ROCK";
                break;

            case Type.PAPER:
                PlayerSelectionText.text = "PAPER";
                break;

            case Type.SCISSORS:
                PlayerSelectionText.text = "SCISSORS";
                break;
        }

    }

    public void MakeAIChoice()
    {

    }

    //===============================================
    //============= BUTTON FUNCTIONS ================
    //===============================================

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnRockClick()
    {
        playerChoice = Type.ROCK;
    }

    public void OnPaperClick()
    {
        playerChoice = Type.PAPER;
    }

    public void OnScissorsClick()
    {
        playerChoice = Type.SCISSORS;
    }

    //================================================
    //================================================
}
