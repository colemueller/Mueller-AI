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
    public int timeValue = 10;
    public GameObject btnGroup;
    public Text timerText;
    public Text trialText;
    public Text PlayerWinText;
    public Text AIWinText;
    public Text PlayerSelectionText;
    public Text AISelectionText;
    public GameObject TieText;
    public GameObject StartButton;

    private int PlayerWins = 0;
    private int AIWins = 0;

    public void OnStartClick()
    {
        TieText.SetActive(false);
        btnGroup.SetActive(true);
        trialNum++;
        timeValue = 10;
        StartCoroutine(CountDown());
        AISelectionText.text = "";
        StartButton.SetActive(false);
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
            timeValue = 10;
            btnGroup.SetActive(false);
            StopAllCoroutines();
            MakeAIChoice();
            StartButton.SetActive(true);
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
        //====================================================================================================================
        //EVALUATION -- LINEAR
        //====================================================================================================================


        //~~~~~~~~~~~~~~ ONE CHOICE ~~~~~~~~~~~~~~~
        //rock most frequent
        if(rockFreq > paperFreq && rockFreq > scissorFreq)
        {
            AIChoice = Type.PAPER;
        }

        //paper most frequent
        else if (paperFreq > rockFreq && paperFreq > scissorFreq)
        {
            AIChoice = Type.SCISSORS;
        }

        //scissor most frequent
        else if (scissorFreq > paperFreq && scissorFreq > rockFreq)
        {
            AIChoice = Type.ROCK;
        }

        //~~~~~~~~~~~~~~ ONE CHOICE END ~~~~~~~~~~~~~~~



        //rock and paper are same
        else if (rockFreq == paperFreq && rockFreq > scissorFreq)
        {
            int temp = Random.Range(1, 2);
            if (temp == 1)
            {
                AIChoice = Type.PAPER;
            }
            else
            {
                AIChoice = Type.SCISSORS;
            }
        }

        //rock and scissors are same
        else if (rockFreq == scissorFreq && rockFreq > paperFreq)
        {
            int temp = Random.Range(1, 2);
            if (temp == 1)
            {
                AIChoice = Type.PAPER;
            }
            else
            {
                AIChoice = Type.ROCK;
            }
        }

        //paper and scissors are same
        else if (paperFreq == scissorFreq && paperFreq > rockFreq)
        {
            int temp = Random.Range(1, 2);
            if (temp == 1)
            {
                AIChoice = Type.SCISSORS;
            }
            else
            {
                AIChoice = Type.ROCK;
            }
        }



        //everything is same frequency
        else
        {
            int temp = Random.Range(1, 3);
            if (temp == 1)
            {
                AIChoice = Type.SCISSORS;
            }
            else if (temp == 2)
            {
                AIChoice = Type.ROCK;
            }
            else
            {
                AIChoice = Type.PAPER;
            }
        }

        //====================================================================================================================
        //EVALUATION -- LINEAR -- END
        //====================================================================================================================



        //change text of AI choice
        switch (AIChoice)
        {
            case Type.ROCK:
                AISelectionText.text = "ROCK";
                break;

            case Type.PAPER:
                AISelectionText.text = "PAPER";
                break;

            case Type.SCISSORS:
                AISelectionText.text = "SCISSORS";
                break;
        }

        //AT END -- learn what player picked, increase its frequency
        switch (playerChoice)
        {
            case Type.ROCK:
                rockFreq++;
                break;

            case Type.PAPER:
                paperFreq++;
                break;

            case Type.SCISSORS:
                scissorFreq++;
                break;
        }

        ChooseWinner();
    }

    public void ChooseWinner()
    {
        switch (playerChoice)
        {
            case Type.ROCK:
                if(AIChoice == Type.PAPER)
                {
                    AIWins++;
                    AIWinText.text = "Wins:" + AIWins.ToString();
                }
                else if (AIChoice == Type.SCISSORS)
                {
                    PlayerWins++;
                    PlayerWinText.text = "Wins:" + PlayerWins.ToString();
                }
                else
                {
                    TieText.SetActive(true);
                }
                break;

            case Type.PAPER:
                if (AIChoice == Type.SCISSORS)
                {
                    AIWins++;
                    AIWinText.text = "Wins:" + AIWins.ToString();
                }
                else if (AIChoice == Type.ROCK)
                {
                    PlayerWins++;
                    PlayerWinText.text = "Wins:" + PlayerWins.ToString();
                }
                else
                {
                    TieText.SetActive(true);
                }
                break;

            case Type.SCISSORS:
                if (AIChoice == Type.ROCK)
                {
                    AIWins++;
                    AIWinText.text = "Wins:" + AIWins.ToString();
                }
                else if (AIChoice == Type.PAPER)
                {
                    PlayerWins++;
                    PlayerWinText.text = "Wins:" + PlayerWins.ToString();
                }
                else
                {
                    TieText.SetActive(true);
                }
                break;
        }
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
