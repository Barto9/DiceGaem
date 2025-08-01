using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public PokerHandEvaluator handEvaluator;
    public Text resultText;
    public Text scoreDisplay;
    public EnemyBase enemy;
    public int score = 0;

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
        score = handEvaluator.EvaluateScore();
        scoreDisplay.text = "Score: " + score;
    }

    public void SubmitDamage()
    {
        enemy.TakeDamage(score);
    }

}