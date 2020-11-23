using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

Esta clase, obtiene la información de la instancia de "StrabismusData" del estrabismo
generado.

Esta clase es usada en la escena "CoverUncover".

La clase EyeMovements obtiene de aquí la información necesaria para establecer 
su comportamiento.


(Posiblemente esta clase se puede eliminar para que cada ojo acceda directamente a StrabismusData
    Por ahora, dejémosla.)

*/

public class EyeSettings : MonoBehaviour
{
    [SerializeField] EyeMovements leftEye;
    [SerializeField] EyeMovements rightEye;

    StrabismusData data;

    public float angleOfDisalignment;


    public string typeOfDeviation;

    public bool isForia;
    public bool isTropia;

    void Awake()
    {
        data = GameObject.Find("StrabismusData").GetComponent<StrabismusData>();
        SetStrabismus();
    }
    void SetStrabismus()
    {
        leftEye.isSquint = data.lefSquint;
        rightEye.isSquint = data.rightSquint;
        isForia = data.isForia;
        isTropia = data.isTropia;
        typeOfDeviation = data.typeOfDeviation;
        angleOfDisalignment = data.angleOfDisalignment;
    }

}
