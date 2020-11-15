using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrabismusData : MonoBehaviour
{


    public float angleOfDisalignment;
    public bool endo;
    public bool exo;
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
