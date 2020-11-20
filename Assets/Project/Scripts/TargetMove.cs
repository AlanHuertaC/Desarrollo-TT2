using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    EyeSettings eyesSet;
    Vector3 eyeR;
    Vector3 eyeL;
    int p = 0;
    // Start is called before the first frame update
    void Start()
    {
        eyesSet = GameObject.Find("Eyes").GetComponent<EyeSettings>();
        int count = eyesSet.transform.childCount;
        eyeR = eyesSet.transform.GetChild(1).localEulerAngles;
        eyeL = eyesSet.transform.GetChild(0).localEulerAngles;
        Debug.Log(eyesSet.transform.GetChild(1).name + eyesSet.transform.GetChild(1).position.ToString("f3"));
        Debug.Log(eyesSet.transform.GetChild(0).name + eyesSet.transform.GetChild(0).position.ToString("f3"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eyesSet.transform.GetChild(1).localEulerAngles != eyeR && p == 0)
        {
            Debug.Log(eyeR);
            Debug.Log(eyesSet.transform.GetChild(1).localEulerAngles);
            p++;
        }
        Movement();
    }

    void Movement()
    {
        //Los GetKey solo estan de prueba para hacerlo manual
        //La condicion del if se cambiara a si la rotacion del ojo no es la misma que la inicial
        if (Input.GetKey(KeyCode.D))
        {
            //El tarjet se movera hasta que la rotacion del ojo vuelva a la original
            transform.Translate(Vector3.right * 10f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * 10f * Time.deltaTime);
    }

}
