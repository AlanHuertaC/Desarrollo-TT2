using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    public string strabismusType;
    public string eye;

    public float angleOfDisalignment;

    static Results instance;
    
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
