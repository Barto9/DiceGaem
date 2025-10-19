using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public Player player;
    public int health = 100;
    public Text lifeDisplay;
    public Text intentDisplay;

    private void Start()
    {
        lifeDisplay.text = "Health:" + health;
        IntentChooser();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        lifeDisplay.text = "Health:" + health;
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
        }
    }
    public void Attack(int damage)
    {
        player.TakeDamage(damage);
    }
    //
    //Intent system
    //
    [System.Serializable]
    public struct EnemyIntent
    {
        public int index;
        public string moveType;
        public int value;
    }
    [SerializeField] private List<EnemyIntent> IntentList = new List<EnemyIntent>();
    public EnemyIntent currentIntent;
    public void IntentChooser()
    {
        int randomIndex = Random.Range(0, IntentList.Count);
        currentIntent = IntentList[randomIndex];
        intentDisplay.text = "Intent: " + currentIntent.moveType + " " + currentIntent.value;
    }
    public void ExecuteIntent()
    {
        switch (currentIntent.moveType)
        {
            case "Attack":
                Attack(currentIntent.value);
                break;
        }
    }
}
