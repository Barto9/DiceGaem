using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokerHandEvaluator : MonoBehaviour
{
    public List<Dice> diceList; // Assign 5 dice in the Inspector
    public string EvaluateHand()
    {
        Dictionary<int, int> faceCounts = new Dictionary<int, int>();

        foreach (var dice in diceList)
        {
            if (faceCounts.ContainsKey(dice.currentValue))
                faceCounts[dice.currentValue]++;
            else
                faceCounts[dice.currentValue] = 1;
        }

        var sortedFaces = faceCounts.Keys.OrderBy(x => x).ToList();

        if (faceCounts.ContainsValue(5) || faceCounts.ContainsValue(6))
            return "Five of a Kind";
        if (faceCounts.ContainsValue(4)) return "Four of a Kind";
        if (faceCounts.ContainsValue(3) && faceCounts.ContainsValue(2)) return "Full House";
        if (sortedFaces.SequenceEqual(new List<int> { 0, 1, 2, 3, 4 }) || sortedFaces.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }))
            return "Straight";
        if (faceCounts.ContainsValue(3)) return "Three of a Kind";
        if (faceCounts.Values.Count(v => v == 2) == 2) return "Two Pair";
        if (faceCounts.ContainsValue(2)) return "One Pair";

        return "High Card";
    }
    public int EvaluateScore()
    {
        return EvaluateHand() switch
        {
            "Five of a Kind" => 100,
            "Four of a Kind" => 25,
            "Full House" => 10,
            "Straight" => 15,
            "Three of a Kind" => 3,
            "Two Pair" => 2,
            "One Pair" => 1,
            "High Card" => 0,
            _ => 0
        };
    }
}