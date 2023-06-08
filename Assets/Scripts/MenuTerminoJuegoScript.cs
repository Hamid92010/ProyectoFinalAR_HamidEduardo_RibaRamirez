using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTerminoJuegoScript : MonoBehaviour
{

    //Criaturas en la escena 
    public GameObject criatura1;
    public GameObject criatura2;
    public GameObject criatura3;
    public GameObject criatura4;
    public GameObject canvas;

    //Componentes Collider de las criaturas
    public CapsuleCollider collider1;
    public CapsuleCollider collider2;
    public CapsuleCollider collider3;
    public CapsuleCollider collider4;
    private bool isColliderActive = true;

    //Indicadores de que han sido detectados
    public bool i1 = false;
    public bool i2 = false;
    public bool i3 = false;
    public bool i4 = false;

    //public ComportamientoBestia vida;
 

    // Start is called before the first frame update
    void Start()
    {
        //Empezará desactivado
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //Instanciar capsule colliders de criaturas
        collider1 = criatura2.GetComponent<CapsuleCollider>();
        collider2 = criatura2.GetComponent<CapsuleCollider>();
        collider3 = criatura3.GetComponent<CapsuleCollider>();
        collider4 = criatura4.GetComponent<CapsuleCollider>();

        // Verificar si el box collider está activo o no
        if (collider1.enabled == isColliderActive)
        {
            i1 = true;
        }
        if (collider2.enabled == isColliderActive)
        {
            i2 = true;
        }
        if (collider3.enabled == isColliderActive)
        {
            i3 = true;
        }
        if (collider4.enabled == isColliderActive)
        {
            i4 = true;
        }
        else
        {
            i1 = false;
            i2 = false;
            i3 = false;
            i4 = false;
        }


        if (i1 == true || i2 == true || i3 == true || i4 == true)
        {
            int vida1 = criatura1.GetComponent<EnemyHealthScript>().enemyHealth;
            int vida2 = criatura2.GetComponent<EnemyHealthScript>().enemyHealth;
            int vida3 = criatura3.GetComponent<EnemyHealthScript>().enemyHealth;
            int vida4 = criatura4.GetComponent<EnemyHealthScript>().enemyHealth;

            if (vida1 <= 0 || vida2 <= 0 || vida3 <= 0 || vida4 <= 0)
            {
                // Inicia una corutina para crear un retardo
                StartCoroutine(DelayedAction());
            }
        }
    }

    IEnumerator DelayedAction()
    {

        // Espera durante 3 segundos
        yield return new WaitForSeconds(4.0f);
        canvas.gameObject.SetActive(true);

    }
}
