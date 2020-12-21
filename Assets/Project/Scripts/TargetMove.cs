using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class TargetMove : MonoBehaviour
{
    EyeSettings eyesSet;
    GameObject pared;

    Results results;
    float primtime = 0;//Esta variable es el tiempo para que se almacene bien la rotacion
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
    [SerializeField] float margenDetener = 0.7f;
    [SerializeField] double angulo = 0;

    Vector3 movmentDirection; //************* Guarda hacia qué lado debe moverse.
    Transform eye;

    float targetAngle;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        eyesSet = GameObject.Find("Eyes").GetComponent<EyeSettings>();
        pared = GameObject.Find("Occluder");
        //Debug.Log(eyesSet.transform.GetChild(1).name + eyesSet.transform.GetChild(1).position.ToString("f3"));
        //Debug.Log(eyesSet.transform.GetChild(0).name + eyesSet.transform.GetChild(0).position.ToString("f3"));
        //Debug.Log("Posicion occluder: " + pared.transform.position); 

        results = GameObject.Find("Results(Clone)").GetComponent<Results>(); //******** Accede a los resultados del coverUncover
        SetMovementDirection(); //***********
        eye = results.eye == "REye" ? eyesSet.transform.GetChild(1) : eyesSet.transform.GetChild(0);
        primtime = 1;
        if(eyesSet.typeOfDeviation == "endo" || eyesSet.typeOfDeviation == "exo") margenDetener = 0.03f;
        else margenDetener = 0.06f;
    }

    // Update is called once per frame
    void Update()
    {
        eulerAngX = eye.localEulerAngles.x;
        eulerAngY = eye.localEulerAngles.y;
        //eulerAngZ = eye.localEulerAngles.z;
        //Debug.Log(eye.transform.name);
        if(eyesSet.typeOfDeviation == "hipo" || eyesSet.typeOfDeviation == "hiper")
            targetAngle = eye.localEulerAngles.x;
        else
            targetAngle = eye.localEulerAngles.y;

        OccluderMovement();
        Movement();
        if(!moving && sectime == 1 && Mathf.Abs(targetAngle - valorinicial) <= margenDetener)CalcularAngulo();
    }


    void SetMovementDirection() ////***************** Guarda hacia qué lado moverse según los resultados
    {
        /*En results ya se indicará hacia qué lado moverse*/
        if(results.direction == "right") movmentDirection = Vector3.right;
        else if(results.direction == "left") movmentDirection = Vector3.left;
        else if(results.direction == "up") movmentDirection = Vector3.up;
        else movmentDirection = Vector3.down;
    }


    void OccluderMovement()
    {
        //Debug.Log(primtime);
        if (primtime < 5) // Espacio entre que empieza la escena y empezar el algoritmo.
        {
            primtime += Time.deltaTime;
        }

        else if (primtime >= 5 && primtime <= 6) // Paso 1. Cerrar vista al ojo sano y dejar ver al ojo desviado.
        {
            if(results.eye == "LEye") pared.transform.position = new Vector3(1.25f, -0.76f, 1.33f); ///****
            else pared.transform.position = new Vector3(-1.25f, -0.76f, 1.33f); //*****
            primtime += Time.deltaTime;
        }

        else if(primtime >= 6 && primtime <= 10) /// Guardar la posición del paso 1 como posición inicial
        {
            valorinicial = eyesSet.typeOfDeviation == "endo" || eyesSet.typeOfDeviation == "exo" 
                                ? eye.localEulerAngles.y : 0;

            primtime += Time.deltaTime;
        }
        else if(primtime > 10 && primtime < 12) /// Paso 2. Cerrar la vista al ojo desviado y abrir al ojo sano.
        {
            if(results.eye == "LEye") pared.transform.position = new Vector3(-1.25f, -0.76f, 1.33f); ///****
            else pared.transform.position = new Vector3(1.25f, -0.76f, 1.33f); //*****
            primtime += Time.deltaTime;
        }
 
        else if ( primtime >= 12) // Paso 3. Empezar a mover el objetivo
        {
 
            sectime = 1;
        }
    }

    void Movement()
    {
        if (sectime == 1 && Mathf.Abs(targetAngle - valorinicial) > margenDetener)
        {
            //El target se movera hasta que la rotacion del ojo vuelva a la original
            transform.Translate(movmentDirection * Time.deltaTime * movementSpeed); //////*************
            moving = true;
        }
        else moving  = false;
    }

    void CalcularAngulo()
    {
        double dt = 24.2; 
        double eyeRadius = 12.1;
        dt *= eyesSet.typeOfDeviation == "endo" || eyesSet.typeOfDeviation == "exo" ? transform.position.x : transform.position.y;
        if (dt < 0)
            dt *= -1f;
        Debug.Log("DT = "+dt);
        double Td = 6000; // 6m
        double PD = 63.5; // 63.5mm
        if(eyesSet.typeOfDeviation == "exo"|| eyesSet.typeOfDeviation == "endo")
        {
            angulo = Math.Atan((2 * dt - PD) / (2 * Td)) + Math.Atan(PD / (2*Td));
            Debug.Log("Radianes: " +angulo);
            angulo = angulo*(180/Math.PI);
            Debug.Log("Grados: " + angulo);
        }
        else
        {
            if(eyesSet.typeOfDeviation == "hiper")  angulo = Math.Atan( (dt + eyeRadius )  / Td);
            else  angulo = Math.Atan( (dt - eyeRadius )  / Td);
            Debug.Log("Radianes: " +angulo);
            angulo = angulo*(180/Math.PI);
            Debug.Log("Grados: " + angulo);
        }

        results.angleOfDisalignment = (float)angulo;
        enableResultButton();
    }

    void enableResultButton()
    {
        Button resultados = GameObject.Find("Resultados").GetComponent<Button>();
        resultados.interactable = true;
    }

}
