using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class MoveWriter : MonoBehaviour
{
//TODO: zapisywanie ruchów do pliku w formacie [odds, stakes, rerolls, killshot, panic]
    [SerializeField] private PokerHandEvaluator handEvaluator;
    [SerializeField] private Player player;
    [SerializeField] private EnemyBase enemy;
    [SerializeField] private Gamemanager gamemanager;
//TODO: dodać przyciski i połączyć je z decision
    public class MoveEntry
    {
        public int Stakes;
        public double Odds;
        public int Rerolls;
        public bool Killshot;
        public bool Panic;
        public bool Decision; //true - reroll, false - submit

        public MoveEntry(double odds, int stakes, int rerolls, bool killshot, bool panic, bool decision)
        {
            Odds = odds;
            Stakes = stakes;
            Rerolls = rerolls;
            Killshot = killshot;
            Panic = panic;
            Decision = decision;
        }

        public override string ToString()
        {
            return $"[{Odds}, {Stakes},{Rerolls},{Killshot}, {Panic}]";
        }
    }

    public List<MoveEntry> MoveEntries = new List<MoveEntry>();



    double CalculateOdds() //szanse, że ruch będzie miał taką samą lub lepszą punktację
    {
        if (handEvaluator == null) return 0.0;
        
        var lockedCounts = GetLockedDiceCount();
        int lockedDiceCount = GetLockedDiceCount();
        int remainingDice = 5 - lockedDiceCount;
        int values = GetLockedDifferentValuesCount();
        double odds = 1 - System.Math.Pow(1 - (values/6), remainingDice);
        return odds;
    }

    // Get count of locked dice
    private int GetLockedDiceCount()
    {
        if (handEvaluator?.diceList == null) return 0;
        return handEvaluator.diceList.Count(d => d.locked);
    }

    // Get count of different values on locked dice
    private int GetLockedDifferentValuesCount()
    {
        if (handEvaluator?.diceList == null) return 0;
        
        var lockedValues = new HashSet<int>();
        foreach (var dice in handEvaluator.diceList)
        {
            if (dice.locked)
            {
                lockedValues.Add(dice.currentValue);
            }
        }
        
        return lockedValues.Count;
    }
    int getStake()
    {
        return handEvaluator != null ? handEvaluator.EvaluateScore() : 0;
    }
    int getRerolls()
    {
        return gamemanager != null ? gamemanager.rollCount : 0;
    }
    bool getKillshot()
    {
        return handEvaluator.EvaluateScore() >= enemy.health;
    }
    bool getPanic()
    {
        if (player != null)
        {
            if (player.health >= enemy.currentIntent.value && enemy.currentIntent.moveType == "Attack" && handEvaluator.EvaluateScore() <= enemy.health)
            {
                return true;
            }

        }
        return false;
    }
}
