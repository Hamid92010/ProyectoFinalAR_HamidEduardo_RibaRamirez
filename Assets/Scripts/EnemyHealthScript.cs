using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{
    public Slider healthBar;
    public int enemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = enemyHealth;
        healthBar.value = enemyHealth;
    }

    public void DamageEnemy(int damageToEnemy)
    {
        enemyHealth -= damageToEnemy;
        healthBar.value = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
