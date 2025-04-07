using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] diceFaces;
    public SpriteRenderer sprite;
    public int currentValue;    // Stores the current dice value
    public bool locked = false;
    public void RollDice()
    {
        currentValue = Random.Range(1, 6);
        sprite.sprite = diceFaces[currentValue - 1];
    }
    public void Lock()
    { locked = true;}
    public void Unlock()
    { locked = false;}
}
