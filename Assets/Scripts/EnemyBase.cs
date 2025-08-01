using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    int life = 100;
    public Text lifeDisplay;

    private void Start()
    {
        lifeDisplay.text = "Life:" + life;
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        lifeDisplay.text = "Life:" + life;
        if (life <= 0)
        {
            Debug.Log("Enemy is dead");
        }
    }

}
