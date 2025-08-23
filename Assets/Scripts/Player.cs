using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 100;
    public Text healthDisplay;
    void Start()
    {
        healthDisplay.text = "Player health:" + health;
    }

    public void TakeDamage(int damage)
    {
        health -=  damage;
        if (health < 0)
        {
            healthDisplay.text = "DED";
        }
        healthDisplay.text = "Player health:" + health;
    }
}
