using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitarAplicacionScript : MonoBehaviour
{
    public void OnPointerClick()
    {
        Application.Quit();
    }
}
