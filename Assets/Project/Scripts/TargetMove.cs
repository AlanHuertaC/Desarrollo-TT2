using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    EyeSettings eyesSet;
    Vector3 eyesCopy;
    int p;

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;
    [SerializeField]
    float valorinicial;

    // Start is called before the first frame update
    void Start()
    {
        eyesSet = GameObject.Find("Eyes").GetComponent<EyeSettings>();
        int count = eyesSet.transform.childCount;
        Debug.Log(eyesSet.transform.GetChild(1).name + eyesSet.transform.GetChild(1).position.ToString("f3"));
        Debug.Log(eyesSet.transform.GetChild(0).name + eyesSet.transform.GetChild(0).position.ToString("f3"));
        eyesCopy = eyesSet.transform.GetChild(1).localEulerAngles;
        Debug.Log("Rotacion actual: " + eyesSet.transform.GetChild(1).localEulerAngles + "Copia: " + eyesCopy);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Rotacion actual: " + eyesSet.transform.GetChild(1).localEulerAngles + "Copia: " + eyesCopy);
        eulerAngX = eyesSet.transform.GetChild(1).localEulerAngles.x;
        eulerAngY = eyesSet.transform.GetChild(1).localEulerAngles.y;
        eulerAngZ = eyesSet.transform.GetChild(1).localEulerAngles.z;
        if (eulerAngY < 30f)
        {
            Debug.Log("Menor que 30");
        }
        else if (p == 0)
        {
            valorinicial = eyesSet.transform.GetChild(1).localEulerAngles.y;
            p++;
        }
        Movement();
    }

    void Movement()
    {
        //Los GetKey solo estan de prueba para hacerlo manual
        if (Input.GetKey(KeyCode.D))
        {
            //El tarjet se movera hasta que la rotacion del ojo vuelva a la original
            transform.Translate(Vector3.left * 10f * Time.deltaTime);
        }
        /*
        else if (eulerAngY > valorinicial)
            transform.Translate(Vector3.right * 10f * Time.deltaTime);
        else if (eulerAngY < valorinicial) 
            transform.Translate(Vector3.left * Time.deltaTime);
        */
    }

    static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

}
