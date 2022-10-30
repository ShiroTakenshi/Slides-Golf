using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Keluar");
    }
}
