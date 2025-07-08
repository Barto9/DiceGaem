using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public PokerHandEvaluator handEvaluator;
    public Text resultText;
    public Text score;

    void Start()
    {
        Debug.Log("GameManager Loaded");
    }

    public void RollAllDice()
    {
        foreach (var dice in handEvaluator.diceList)
            dice.RollDice();

        resultText.text = "Hand: " + handEvaluator.EvaluateHand();
        score.text = "Score: " + handEvaluator.EvaluateScore();
        
    }
}