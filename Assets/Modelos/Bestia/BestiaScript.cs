using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiaScript : MonoBehaviour
{
    public Animator animatorObjetivo;
    public string Atacar = "Attack";
    public string Iddle = "Iddle";
    public string Morir = "Die";

    public GameObject escudo;

    // Start is called before the first frame update
    void Start()
    {
        animatorObjetivo.SetBool("Attack", false);
        animatorObjetivo.SetBool("Iddle", true);
        animatorObjetivo.SetBool("Die", false);
    }

    // Update is called once per frame
    void Update()
    {
        /*prueba
        escudo.SetActive(false);
        animatorObjetivo.SetBool("Attack", true);
        animatorObjetivo.SetBool("Iddle", false);
        animatorObjetivo.SetBool("Die", false);
        */
        
    }

    public void ActivarIddle()
    {
        escudo.SetActive(true);
        animatorObjetivo.SetBool("Attack", false);
        animatorObjetivo.SetBool("Iddle", true);
        animatorObjetivo.SetBool("Die", false);
    }

    public void ActivarAtaque()
    {
        escudo.SetActive(false);
        animatorObjetivo.SetBool("Attack", true);
        animatorObjetivo.SetBool("Iddle", false);
        animatorObjetivo.SetBool("Die", false);
    }
}
