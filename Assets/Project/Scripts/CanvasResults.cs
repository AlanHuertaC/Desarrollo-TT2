using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasResults : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform panel; 
    float num =0f;
    void Start()
    {
        panel= GetComponent<RectTransform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(num<800)
        panel.transform.position = new Vector3(512,1185-(num+=3.5f),0);
    }
}
