using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class MoveWriter : MonoBehaviour
{
    [SerializeField] private PokerHandEvaluator handEvaluator;
    [SerializeField] private Player player;
    [SerializeField] private EnemyBase enemy;
    public class MoveEntry
    {
        public int Stakes;
        public float Odds;
        public int Rerolls;
        public bool Killshot;
        public bool Panic;
        public bool Decision; //true - reroll, false - submit

        public MoveEntry(float odds, int stakes, int rerolls, bool killshot, bool panic, bool decision)
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
            return $"{Odds.ToString(System.Globalization.CultureInfo.InvariantCulture)},{Stakes},{Rerolls},{Killshot},{Panic},{Decision}";
        }
    }

    public List<MoveEntry> MoveEntries = new List<MoveEntry>();
    private string filePath;

    void Start()
    {
        // Set up file path in project folder
        filePath = Path.Combine(Application.dataPath, "..", "move_data.csv");
        
        // Write CSV header if file doesn't exist
        if (!File.Exists(filePath))
        {
            WriteHeader();
        }
    }

    private void WriteHeader()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Odds,Stakes,Rerolls,Killshot,Panic,Decision");
        }
    }

    public void SaveMoveEntry(MoveEntry entry)
    {
        MoveEntries.Add(entry);
        
        // Append to CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(entry.ToString());
        }
        
        Debug.Log($"Move saved: {entry}");
    }

    public void SaveAllMoves()
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine("Odds,Stakes,Rerolls,Killshot,Panic,Decision");
            
            foreach (var entry in MoveEntries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
        
        Debug.Log($"All {MoveEntries.Count} moves saved to {filePath}");
    }

    public float CalculateOdds() // szanse, że ruch będzie miał taką samą lub lepszą punktację
    {
        if (handEvaluator == null)
        {
            Debug.Log("handEvaluator is null");
            return 0f;
        }

        int lockedDiceCount = GetLockedDiceCount();
        int remainingDice = 5 - lockedDiceCount;
        int values = GetLockedDifferentValuesCount();

        Debug.Log($"Locked dice: {lockedDiceCount}, Remaining: {remainingDice}, Different values: {values}");

        // Compute odds using float math
        float odds = 1f - Mathf.Pow(1f - (values / 6f), remainingDice);

        // Round to 2 decimal places
        float oddsRounded = (float)System.Math.Round(odds, 2);

        return oddsRounded;
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
    public bool getKillshot()
    {
        return handEvaluator.EvaluateScore() >= enemy.health;
    }
    public bool getPanic()
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
