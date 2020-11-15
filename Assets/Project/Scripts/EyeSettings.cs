using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSettings : MonoBehaviour
{
    [SerializeField] EyeMovements leftEye;
    [SerializeField] EyeMovements rightEye;

    StrabismusData data;

    public float angleOfDisalignment;


    public bool endo;
    public bool exo;
    

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
        endo = data.endo;
        exo = data.exo;
        angleOfDisalignment = data.angleOfDisalignment;
    }

}
