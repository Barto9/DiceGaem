using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public PokerHandEvaluator handEvaluator;
    public Text resultText;
    public Text scoreDisplay;
    public EnemyBase enemy;
    public int score = 0;
    public int rollCount;
    public Text rollCountDisplay;
    void Start()
    {
        rollCount = 3;
        Debug.Log("GameManager Loaded"); // To confirm the script is running
    }

    public void RollAllDice()
    {
        if (rollCount <= 0)
        {
            Debug.Log("Out of rolls");
            return;
        }
        foreach (var dice in handEvaluator.diceList)
            if (!dice.locked)
            {
                dice.RollDice();
            }
        rollCount--;
        resultText.text = "Hand: " + handEvaluator.EvaluateHand();
        score = handEvaluator.EvaluateScore();
        scoreDisplay.text = "Score: " + score;
        rollCountDisplay.text = "Rolls " + rollCount;
    }

    public void SubmitDamage()
    {
        if (rollCount == 3)
        { 
            Debug.Log("Cannot Submit. Dice not rolled, or roll has been already submitted.");
            return;
        }
        enemy.TakeDamage(score);
        rollCount = 3;
        foreach (var dice in handEvaluator.diceList)
        {
            dice.Unlock();
            Debug.Log("Dice unlocked");
        }
    }

}