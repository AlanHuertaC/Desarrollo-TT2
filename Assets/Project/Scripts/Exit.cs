using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    //Exit Application
    public void exit()
    {
        Application.Quit();
        Debug.Log("Salir");
    }
}
