using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyScript : MonoBehaviour
{
    public int damageToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2); //espera 5 segundos en ser destruido
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Golem") || collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("Hechicero") || collision.gameObject.CompareTag("Bestia"))
        {
            collision.gameObject.GetComponent<EnemyHealthScript>().DamageEnemy(damageToPlayer);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
