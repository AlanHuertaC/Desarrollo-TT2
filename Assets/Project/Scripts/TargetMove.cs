using System.Collections;
using System.Collections.Generic;
using System;
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
    [SerializeField] float margenError = 0.7f;
    [SerializeField] double angulo = 0;

    Vector3 movmentDirection; //************* Guarda hacia qué lado debe moverse.
    Transform eye;

    bool moving = false;

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
        OccluderMovement();
        Movement();
        if(!moving && sectime == 1 && Mathf.Abs(eulerAngY - valorinicial) <= margenError)CalcularAngulo();
    }

    void Movement()
    {
        if (sectime == 1 && Mathf.Abs(eulerAngY - valorinicial) > margenError)
        {
            //El tarjet se movera hasta que la rotacion del ojo vuelva a la original
            transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //////*************
            moving = true;
        }
        else moving  = false;
            /*else if (sectime == 1 && eulerAngY > valorinicial + 2.5f)
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //*****************}*
        /*
            if (sectime == 1 && Mathf.Abs(eulerAngY - valorinicial) > 0.07)
            {
                //El tarjet se movera hasta que la rotacion del ojo vuelva a la original
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //////************* 
            }
            /*else if (sectime == 1 && eulerAngY > valorinicial + 2.5f)
                transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //*****************}*/
     

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
        double dt = 24.2 * transform.position.x;
        if (dt < 0)
            dt *= -1f;
        Debug.Log("DT = "+dt);
        double Td = 6000; // 6m
        double PD = 63.5; // 63.5mm
        double datos = 2 * dt;
        angulo = Math.Atan((2 * dt - PD) / (2 * Td)) + Math.Atan(PD / (2*Td));
        Debug.Log("Radianes: " +angulo);
        angulo = angulo*(180/Math.PI);
        Debug.Log("Grados: " + angulo);

        results.angleOfDisalignment = (float)angulo;
    }


    void SetMovementDirection() ////***************** Guarda hacia qué lado moverse según los resultados
    {
        /*En results ya se indicará hacia qué lado moverse*/
        if(results.direction == "right") movmentDirection = Vector3.right;
        else movmentDirection = Vector3.left;
    }


    void OccluderMovement()
    {
        if (primtime < 300) // Espacio entre que empieza la escena y empezar el algoritmo.
        {
            //Debug.Log("Wenas :v " + valorinicial);
            primtime++;
        }
        else if (primtime == 300) // Paso 1. Cerrar vista al ojo sano y dejar ver al ojo desviado.
        {
            if(results.eye == "LEye") pared.transform.position = new Vector3(1.25f, -0.76f, 1.33f); ///****
            else pared.transform.position = new Vector3(-1.25f, -0.76f, 1.33f); //*****
            primtime++;
        }
        else if(primtime > 300 && primtime < 600) /// Guardar la posición del paso 1 como posición inicial
        {
            valorinicial = eulerAngY;
            primtime++;
        }
        else if(primtime == 600) /// Paso 2. Cerrar la vista al ojo desviado y abrir al ojo sano.
        {
            if(results.eye == "LEye") pared.transform.position = new Vector3(-1.25f, -0.76f, 1.33f); ///****
            else pared.transform.position = new Vector3(1.25f, -0.76f, 1.33f); //*****
            primtime++;
        }
        else if (eulerAngY + 2f != valorinicial && primtime >= 601) // Paso 3. Empezar a mover el objetivo
        {
           // Debug.Log("Menor que 30");
            sectime = 1;
        }

    }

}
