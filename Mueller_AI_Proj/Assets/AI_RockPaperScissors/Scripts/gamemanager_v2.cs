using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager_v2 : MonoBehaviour {


    public enum Type { ROCK, PAPER, SCISSORS, NULL};
    public Type playerChoice;
    public Type AIChoice;
    public Type LastChoice = Type.NULL;

    public float rockProb = 0.35f;
    public float paperProb = 0.35f;
    public float scissorProb = 0.3f;

    public static int trialNum = 0;
    public int timeValue = 10;
    public GameObject btnGroup;
    public Text timerText;
    public Text trialText;
    public Text PlayerWinText;
    public Text PlayerWinMessage;
    public Text AIWinText;
    public Text AIWinMessage;
    public Text PlayerSelectionText;
    public Text AISelectionText;
    public GameObject TieText;
    public GameObject StartButton;
    public GameObject DoneButton;

    private int PlayerWins = 0;
    private int AIWins = 0;

    private bool tog = false;
    public GameObject probGroup;

    public Text rockProbText;
    public Text paperProbText;
    public Text scissorsProbText;

    

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

        rockProbText.text = "Rock: " + (rockProb * 100).ToString() + "%";
        paperProbText.text = "Paper: " + (paperProb * 100).ToString() + "%";
        scissorsProbText.text = "Scissors: " + (scissorProb * 100).ToString() + "%";

        if (timeValue <= 0)
        {
            timeValue = 10;
            btnGroup.SetActive(false);
            StopAllCoroutines();
            MakeAIChoice();
            StartButton.SetActive(true);
            DoneButton.SetActive(false);
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
        //EVALUATION -- Psuedo-Bayesian
        //====================================================================================================================


        //~~~~~~~~~~~~~~ ONE CHOICE ~~~~~~~~~~~~~~~
        //rock most frequent
        if(rockProb > paperProb && rockProb > scissorProb)
        {
            AIChoice = Type.PAPER;
        }

        //paper most frequent
        else if (paperProb > rockProb && paperProb > scissorProb)
        {
            AIChoice = Type.SCISSORS;
        }

        //scissor most frequent
        else if (scissorProb > paperProb && scissorProb > rockProb)
        {
            AIChoice = Type.ROCK;
        }

        //~~~~~~~~~~~~~~ ONE CHOICE END ~~~~~~~~~~~~~~~


        //NEW: now has a potential tie-breaker with LastChoice evaluation
        //rock and paper are same
        else if (rockProb == paperProb && rockProb > scissorProb)
        {
            
            if (LastChoice == Type.ROCK)  //players last choice was rock. AI assumes next choice will be also be rock
            {
                AIChoice = Type.PAPER;
            }
            else if (LastChoice == Type.PAPER)  //players last choice was paper. AI assumes next choice will be also be paper
            {
                AIChoice = Type.SCISSORS;
            }
            else //choose random
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
        }

        //rock and scissors are same
        else if (rockProb == scissorProb && rockProb > paperProb)
        {
            if(LastChoice == Type.ROCK)
            {
                AIChoice = Type.PAPER;
            }
            else if (LastChoice == Type.SCISSORS)
            {
                AIChoice = Type.ROCK;
            }
            else { 
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
        }

        //paper and scissors are same
        else if (paperProb == scissorProb && paperProb > rockProb)
        {
            if(LastChoice == Type.PAPER)
            {
                AIChoice = Type.SCISSORS;
            }
            else if (LastChoice == Type.SCISSORS)
            {
                AIChoice = Type.ROCK;
            }
            else { 
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
        }



        //everything is same frequency
        else
        {
            if (LastChoice == Type.ROCK)
            {
                AIChoice = Type.PAPER;
            }
            else if (LastChoice == Type.PAPER)
            {
                AIChoice = Type.SCISSORS;
            }
            else if(LastChoice == Type.SCISSORS)
            {
                AIChoice = Type.ROCK;
            }
            else { 
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

        //AT END -- learn what player picked, increase its frequency - NEW: values are all relative to each other
        switch (playerChoice)
        {
            case Type.ROCK:
                if (rockProb < 1)
                {
                    rockProb += 0.1f;
                    paperProb -= 0.05f;
                    scissorProb -= 0.05f;
                    
                }
                break;

            case Type.PAPER:
                if (paperProb < 1)
                {
                    paperProb += 0.1f;
                    rockProb -= 0.05f;
                    scissorProb -= 0.05f;

                }
                break;

            case Type.SCISSORS:
                if (scissorProb < 1)
                {
                    scissorProb += 0.1f;
                    paperProb -= 0.05f;
                    rockProb -= 0.05f;

                }
                break;
        }

        ChooseWinner();
        LastChoice = playerChoice;
    }

    public void ChooseWinner()
    {
        switch (playerChoice)
        {
            case Type.ROCK:
                if(AIChoice == Type.PAPER)
                {
                    AIWins++;
                    AIWinMessage.text = "AI wins...";
                    AIWinText.text = "Wins:" + AIWins.ToString();
                }
                else if (AIChoice == Type.SCISSORS)
                {
                    PlayerWins++;
                    PlayerWinMessage.text = "You Win!";
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
                    AIWinMessage.text = "AI wins...";
                    AIWinText.text = "Wins:" + AIWins.ToString();
                }
                else if (AIChoice == Type.ROCK)
                {
                    PlayerWins++;
                    PlayerWinMessage.text = "You Win!";
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
                    AIWinMessage.text = "AI wins...";
                    AIWinText.text = "Wins:" + AIWins.ToString();
                }
                else if (AIChoice == Type.PAPER)
                {
                    PlayerWins++;
                    PlayerWinMessage.text = "You Win!";
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

    public void OnStartClick()
    {
        TieText.SetActive(false);
        btnGroup.SetActive(true);
        trialNum++;
        timeValue = 10;
        StartCoroutine(CountDown());
        AISelectionText.text = "";
        StartButton.SetActive(false);
        DoneButton.SetActive(true);
        PlayerWinMessage.text = "";
        AIWinMessage.text = "";
    }

    public void OnDoneClick()
    {
        StopAllCoroutines();
        timeValue = 0;
    }

    public void OnToggleClick()
    {
        if (tog)
        {
            probGroup.SetActive(true);
        }
        else
        {
            probGroup.SetActive(false);
        }

        tog = !tog;
    }

    //================================================
    //================================================
}
