using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    EyeSettings eyesSet;
    GameObject pared;

    Results results;
    int primtime = 0;//Esta variable es el tiempo para que se almacene bien la rotacion
    int sectime = 0;//Esta variable es cuando una vez pasado el tiempo ya puede moverse el Tarjet

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;
    [SerializeField]
    float valorinicial;

    [SerializeField] float movementSpeed = 5f; 

    Vector3 movmentDirection; //************* Guarda hacia qué lado debe moverse.
    Transform eye;

    // Start is called before the first frame update
    void Start()
    {
        eyesSet = GameObject.Find("Eyes").GetComponent<EyeSettings>();
        pared = GameObject.Find("Occluder");
        Debug.Log(eyesSet.transform.GetChild(1).name + eyesSet.transform.GetChild(1).position.ToString("f3"));
        Debug.Log(eyesSet.transform.GetChild(0).name + eyesSet.transform.GetChild(0).position.ToString("f3"));
        Debug.Log("Posicion occluder: " + pared.transform.position); 

        results = GameObject.Find("Results(Clone)").GetComponent<Results>(); //******** Accede a los resultados del coverUncover
        SetMovementDirection(); //***********

        eye = results.eye == "REye" ? eyesSet.transform.GetChild(1) : eyesSet.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        /*eulerAngX = eyesSet.transform.GetChild(1).localEulerAngles.x;
        eulerAngY = eyesSet.transform.GetChild(1).localEulerAngles.y;
        eulerAngZ = eyesSet.transform.GetChild(1).localEulerAngles.z;*/

        eulerAngX = eye.localEulerAngles.x;
        eulerAngY = eye.localEulerAngles.y;
        eulerAngZ = eye.localEulerAngles.z;

        //eulerAngY + 2f < valorinicial &&
        if (eulerAngY + 2f != valorinicial && primtime >= 161)
        {
            Debug.Log("Menor que 30");
            sectime = 1;
        }
        else if (primtime < 160)
        {
            valorinicial = eulerAngY;
            Debug.Log("Wenas :v " + valorinicial);
            primtime++;
        }
        else if (primtime == 160)
        {
            if(results.eye == "LEye") pared.transform.position = new Vector3(1.42f, 0.816f, 0.8f); ///****
            else pared.transform.position = new Vector3(-1.42f, 0.816f, 0.8f); //*****
            primtime++;
        }
        Movement();
    }

    void Movement()
    {
        if(results.direction == "right")
        {    if (sectime == 1 && eulerAngY < valorinicial)
            {
                //El tarjet se movera hasta que la rotacion del ojo vuelva a la original
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //////************* 
            }
            else if (sectime == 1 && eulerAngY > valorinicial + 2.5f)
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //*****************}
        }
        else
        {
            if (sectime == 1 && eulerAngY > valorinicial)
            {
                //El tarjet se movera hasta que la rotacion del ojo vuelva a la original
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //////************* 
            }
            else if (sectime == 1 && eulerAngY > valorinicial + 2.5f)
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //*****************}
        } 

        /* SÓLO NO JALA LA EXO DEL OJO IZQUIERO XD */
        
           
        /*
        //Si el angulo es menor que el inicial, se movera a la derecha de nuestra vista
        else if (eulerAngY < valorinicial)
            transform.Translate(Vector3.left * 10f * Time.deltaTime);
        //Si el angulo es mayor que el inicial, se movera a la izquierda de nuestra vista
        else if (eulerAngY > valorinicial)
            transform.Translate(Vector3.right * 10f * Time.deltaTime);
        */
    }

    void CalcularAngulo()
    {
        float dt = 24.2f * transform.position.x;
        if (dt < 0)
            dt *= -1f;
        float Td = 400f;
        float PD = 24.2f * 2.60f;
        float angulo = Mathf.Atan((2f * dt - PD) / 2f * Td) + Mathf.Atan(PD/(2f * Td));
    }


    void SetMovementDirection() ////***************** Guarda hacia qué lado moverse según los resultados
    {
        /*En results ya se indicará hacia qué lado moverse*/
        if(results.direction == "right") movmentDirection = Vector3.right;
        else movmentDirection = Vector3.left;
    }

}
