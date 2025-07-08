using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public PokerHandEvaluator handEvaluator;
    public Text resultText;
    public Text score;

    void Start()
    {
        Debug.Log("GameManager Loaded"); // To confirm the script is running
    }

    public void RollAllDice()
    {
        foreach (var dice in handEvaluator.diceList)
            if (!dice.locked)
            {
                dice.RollDice();
            }

        resultText.text = "Hand: " + handEvaluator.EvaluateHand();
        score.text = "Score: " + handEvaluator.EvaluateScore();
    }
}