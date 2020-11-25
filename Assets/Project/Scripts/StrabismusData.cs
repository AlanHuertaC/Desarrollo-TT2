using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Esta clase guarda la información del estrabismo generado en la escena "SetStrabismus".

A través de esta las otras escenas pueden acceder a la información que se generó.

*/

public class StrabismusData : MonoBehaviour
{


    public float angleOfDisalignment;
    public string typeOfDeviation;
    public bool isForia;
    public bool isTropia;

    public bool lefSquint;
    public bool rightSquint;
    

    static StrabismusData instance;
    
    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }


   
}
