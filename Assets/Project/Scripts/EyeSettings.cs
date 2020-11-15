using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSettings : MonoBehaviour
{
    [SerializeField] EyeMovements leftEye;
    [SerializeField] EyeMovements rightEye;

    [SerializeField] float maxAngle = 25f;

    public float angleOfDisalignment;


    public bool endo;
    public bool exo;
    

    public bool isForia;
    public bool isTropia;

    void Awake()
    {
        SetStrabismus();
    }
    void SetStrabismus()
    {

        leftEye.isSquint = false;
        rightEye.isSquint = false;
        float random = Random.value;
        Debug.Log(random);
        if(random > 0.5) rightEye.isSquint = true;
        else leftEye.isSquint = true;

        isForia = false;
        isTropia = false;
        random = 0.51f; //////////////////////// MUST BE RANDOM.
        if(random > 0.5) isTropia = true;
        else isForia = true;

        exo = false;
        endo = false;
        random =Random.value; 
        if(random > 0.5) exo = true;
        else endo = true;        

        angleOfDisalignment = Random.value * maxAngle;
        if(endo && rightEye.isSquint || exo && leftEye.isSquint) angleOfDisalignment *= -1;
    }

}
