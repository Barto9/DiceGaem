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
    public Text debugText;
    public MoveWriter moveWriter;
    void Start()
    {
        rollCount = 3;
        debugText.text = "Welcome to Dicegaem";
        scoreDisplay.text = "Score:";
        rollCountDisplay.text = "Rolls:";
        resultText.text = "Hand:";
    }

    public void RollAllDice()
    {
        if (rollCount == 3)
        {
            foreach (var dice in handEvaluator.diceList)
                if (!dice.locked)
                {
                    dice.Unlock();
                }
        }
       

        if (rollCount <= 0)
        {
            debugText.text = "Out of rolls";
            return;
        } 
        //save move entry with decision true (reroll)
        if (rollCount !=3)
        {
        moveWriter.SaveMoveEntry(new MoveWriter.MoveEntry(moveWriter.CalculateOdds(), score, rollCount, moveWriter.getKillshot(), moveWriter.getPanic(), true));
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
        rollCountDisplay.text = "Rolls: " + rollCount;
    }

    public void SubmitDamage()
    {
        if (rollCount == 3)
        { 
            debugText.text = "Cannot Submit. Dice not rolled.";
            return;
        }
        //save move entry with decision false (submit)
        moveWriter.SaveMoveEntry(new MoveWriter.MoveEntry(moveWriter.CalculateOdds(), score, rollCount, moveWriter.getKillshot(), moveWriter.getPanic(), false));
        rollCount = 3;
        rollCountDisplay.text = "Rolls: " + rollCount;
        enemy.TakeDamage(score);
        enemy.ExecuteIntent();
        
        foreach (var dice in handEvaluator.diceList)
        {
            dice.Unlock();
        }
    }

}