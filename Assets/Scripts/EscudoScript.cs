using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoScript : MonoBehaviour
{
    public GameObject miObjeto;
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (miObjeto.activeSelf)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
    }
}
