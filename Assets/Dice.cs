using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] diceFaces;  // Assign six images in the Inspector
    public SpriteRenderer sprite;       //TU ZMIENI£AM
    public int currentValue;    // Stores the current dice value

    public void RollDice()
    {
        currentValue = Random.Range(1, 6); // Random number from 0 to 5
        sprite.sprite = diceFaces[currentValue - 1];       // Update UI Image       //NO I TU
    }
}
