using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MoveWriter : MonoBehaviour
{
    [SerializeField] private PokerHandEvaluator handEvaluator;
    [SerializeField] private Player player;
    [SerializeField] private EnemyBase enemy;
    [SerializeField] private Gamemanager gamemanager;
    public class MoveEntry
    {
        public double Odds;
        public int Stakes;
        public double Payout;
        public int Rerolls;
        public int PlayerHp;
        public int EnemyHp;
        public bool Panic;

        public MoveEntry(double odds, int stakes, double payout, int rerolls, int playerHp, int enemyHp, bool panic)
        {
            Odds = odds;
            Stakes = stakes;
            Payout = payout;
            Rerolls = rerolls;
            PlayerHp = playerHp;
            EnemyHp = enemyHp;
            Panic = panic;
        }

        public override string ToString()
        {
            return $"[{Odds}, {Stakes}, {Payout}, {Rerolls}, {PlayerHp}, {EnemyHp}, {Panic}]";
        }
    }

    public List<MoveEntry> MoveEntries = new List<MoveEntry>();



    double CalculateOdds() //szanse, że ruch będzie miał taką samą lub leprzą punktację
    {
        //this is gonna be a mess to include locked dice
        return 0.0;
    }
    int getStake()
    {
        return handEvaluator != null ? handEvaluator.EvaluateScore() : 0;
    }
    int getRerolls()
    {
        return gamemanager != null ? gamemanager.rollCount : 0;
    }
    int getPlayerHp()
    {
        return player != null ? player.health : 0;
    }
    int getEnemyHp()
    {
        return enemy != null ? enemy.health : 0;
    }
    bool getPanic()
    {
        if (player!= null)
        {
            if (player.health >= enemy.currentIntent.value && enemy.currentIntent.moveType == "Attack" && handEvaluator.EvaluateScore() <= enemy.health)
            {
                return true;
            }
            
        }
        return false;
    }
}
