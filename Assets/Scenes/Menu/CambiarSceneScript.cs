using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarSceneScript : MonoBehaviour
{
    public string nombreDeLaEscena; // Nombre de la escena a cambiar

    public void OnPointerClick()
    {
        SceneManager.LoadScene(nombreDeLaEscena); // Cambiar a la escena especificada
    }
}


