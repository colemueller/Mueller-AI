using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager_v2 : MonoBehaviour {


    public enum Type { ROCK, PAPER, SCISSORS, NULL};
    public Type playerChoice = Type.NULL;
    public Type AIChoice;
    public Type PlayerTell = Type.NULL;
    public Type AITell;

    [Range(0,100)]
    public int rockProb = 35;
    [Range(0, 100)]
    public int paperProb = 35;
    [Range(0, 100)]
    public int scissorProb = 30;

    public static int trialNum = 0;
    public int timeValue = 10;
    public GameObject btnGroup;
    public GameObject TellBtnGroup;
    public Text timerText;
    public Text trialText;
    public Text PlayerWinText;
    public Text PlayerWinMessage;
    public Text AIWinText;
    public Text AIWinMessage;
    public Text PlayerSelectionText;
    public Text PlayerTellText;
    public Text AISelectionText;
    public Text AISelectionTitle;
    public GameObject TieText;
    public GameObject StartButton;
    public GameObject DoneButton;
    public GameObject PlayerTellGroup;

    private int PlayerWins = 0;
    private int AIWins = 0;

    private bool tog = false;
    public GameObject probGroup;

    public Text rockProbText;
    public Text paperProbText;
    public Text scissorsProbText;
    public Text randomText;

    public bool DoPlayerTell = true;
    public bool DoIterationOverTime = false;

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

        rockProbText.text = "Rock: " + rockProb.ToString() + "%";
        paperProbText.text = "Paper: " + paperProb.ToString() + "%";
        scissorsProbText.text = "Scissors: " + scissorProb.ToString() + "%";

        if (timeValue <= 0)
        {
            timeValue = 10;
            btnGroup.SetActive(false);
            TellBtnGroup.SetActive(false);
            StopAllCoroutines();
            MakeAIChoice();
            StartButton.SetActive(true);
            DoneButton.SetActive(false);
            AISelectionTitle.text = "AI Selection";


        }

        if(DoPlayerTell == false)
        {
            TellBtnGroup.SetActive(false);
            PlayerTellGroup.SetActive(false);
        }
        else
        {
            PlayerTellGroup.SetActive(true);
        }

        //change text
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
        //change text
        switch (PlayerTell)
        {
            case Type.ROCK:
                PlayerTellText.text = "ROCK";
                break;

            case Type.PAPER:
                PlayerTellText.text = "PAPER";
                break;

            case Type.SCISSORS:
                PlayerTellText.text = "SCISSORS";
                break;
        }

        //DONT GO BELOW ZERO
        if(rockProb < 0)
        {
            rockProb = 0;
        }
        if (paperProb < 0)
        {
            paperProb = 0;
        }
        if (scissorProb < 0)
        {
            scissorProb = 0;
        }

    }

    public void MakeAIChoice()
    {
        //Change probabilities based off of player tell and AI tell -- can toggle in game
        if (DoPlayerTell) 
        {
            switch (AITell)
            {

                //AI shows ROCK
                case Type.ROCK:
                    if (paperProb <= 90)
                    {
                        paperProb += 10;
                        scissorProb -= 10;
                    }
                    switch (PlayerTell)
                    {
                        //Player shows rock
                        case Type.ROCK:
                            if(rockProb <= 90)
                            {
                                rockProb += 10;
                                paperProb -= 10;
                            }
                            break;
                        case Type.PAPER:
                            if (paperProb <= 90)
                            {
                                paperProb += 10;
                                scissorProb -= 10;
                            }
                            break;
                        case Type.SCISSORS:
                            if (scissorProb <= 90)
                            {
                                scissorProb += 10;
                                rockProb -= 10;
                            }
                            break;
                    }
                    break;

                //AI shows PAPER
                case Type.PAPER:
                    if (scissorProb <= 90)
                    {
                        scissorProb += 10;
                        rockProb -= 10;
                    }
                    switch (PlayerTell)
                    {
                        case Type.ROCK:
                            if (rockProb <= 90)
                            {
                                rockProb += 10;
                                paperProb -= 10;
                            }
                            break;
                        case Type.PAPER:
                            if (paperProb <= 90)
                            {
                                paperProb += 10;
                                scissorProb -= 10;
                            }
                            break;
                        case Type.SCISSORS:
                            if (scissorProb <= 90)
                            {
                                scissorProb += 10;
                                rockProb -= 10;
                            }
                            break;
                    }
                    break;

                //AI shows SCISSORS
                case Type.SCISSORS:
                    if (rockProb <= 90)
                    {
                        rockProb += 10;
                        paperProb -= 10;
                    }
                    switch (PlayerTell)
                    {
                        case Type.ROCK:
                            if (rockProb <= 90)
                            {
                                rockProb += 10;
                                paperProb -= 10;
                            }
                            break;
                        case Type.PAPER:
                            if (paperProb <= 90)
                            {
                                paperProb += 10;
                                scissorProb -= 10;
                            }
                            break;
                        case Type.SCISSORS:
                            if (scissorProb <= 90)
                            {
                                scissorProb += 10;
                                rockProb -= 10;
                            }
                            break;
                    }
                    break;
            }
        }
        //Only change prob off of AI tell
        else
        { 
            switch (AITell)
            {
                case Type.ROCK: //Player sees that the AI is thinking about playing rock.
                    if(paperProb <= 90)
                    {
                        paperProb += 10; //Probability the player will play the winning choice goes up
                        scissorProb -= 10; //Probability the player will play the losing choice goes down
                                           //Probability the player will play the neutral choice stays put
                    }
                    break;
                case Type.PAPER:
                    if (scissorProb <= 90)
                    {
                        scissorProb += 10;
                        rockProb -= 10;
                    }
                    break;
                case Type.SCISSORS:
                    if (rockProb <= 90)
                    {
                        rockProb += 10;
                        paperProb -= 10;
                    }
                    break;
            }
        }


        //====================================================================================================================
        //EVALUATION -- TRUE PROBABILITY -- START
        //====================================================================================================================

        int num = Random.Range(0,(rockProb + paperProb + scissorProb));
        randomText.text = "Num: " + num.ToString();

        //Random num falls within rock probability
        if(0f <= num && num <= rockProb)
        {
            AIChoice = Type.PAPER;
        }
        //Random num falls within paper probability
        else if (rockProb < num && num <= (rockProb + paperProb))
        {
            AIChoice = Type.SCISSORS;
        }
        //Random num falls within scissors probability
        else
        {
            AIChoice = Type.ROCK;
        }


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

        //AT END -- learn what player picked, increase its frequency - NEW: values are all relative to each other -- Can Toggle
        if (DoIterationOverTime)
        {
            switch (playerChoice)
            {
                case Type.ROCK:
                    if (rockProb < 100)
                    {
                        rockProb += 10;
                        paperProb -= 5;
                        scissorProb -= 5;

                    }
                    break;

                case Type.PAPER:
                    if (paperProb < 100)
                    {
                        paperProb += 10;
                        rockProb -= 5;
                        scissorProb -= 5;

                    }
                    break;

                case Type.SCISSORS:
                    if (scissorProb < 100)
                    {
                        scissorProb += 10;
                        paperProb -= 5;
                        rockProb -= 5;

                    }
                    break;
            }
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

    public void OnRockTellClick()
    {
        PlayerTell = Type.ROCK;
    }

    public void OnPaperTellClick()
    {
        PlayerTell = Type.PAPER;
    }

    public void OnScissorsTellClick()
    {
        
        PlayerTell = Type.SCISSORS;
    }

    public void OnStartClick()
    {
        int i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                AITell = Type.ROCK;
                AISelectionText.text = "ROCK";
                break;
            case 2:
                AITell = Type.PAPER;
                AISelectionText.text = "PAPER";
                break;
            case 3:
                AITell = Type.SCISSORS;
                AISelectionText.text = "SCISSORS";
                break;
        }
        TieText.SetActive(false);
        btnGroup.SetActive(true);
        TellBtnGroup.SetActive(true);
        trialNum++;
        timeValue = 10;
        StartCoroutine(CountDown());
       
        StartButton.SetActive(false);
        DoneButton.SetActive(true);
        PlayerWinMessage.text = "";
        AIWinMessage.text = "";
        AISelectionTitle.text = "AI Tell";
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

    public void OnPlayerTellToggle()
    {
        DoPlayerTell = !DoPlayerTell;
    }

    //================================================
    //================================================
}
