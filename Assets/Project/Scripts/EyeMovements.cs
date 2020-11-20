using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Clase encargada de establecer el comportamiento de cada ojo dependiendo de su estado
    (con estrabismo o sin estrabismo).
*/
public class EyeMovements : MonoBehaviour
{
    Transform target; // referencia a Transform del objetivo

    EyeSettings eyeSettings; // referencia al objeto de la clase EyeSettings para obtener los datos del estrabismo
    public Quaternion deviation; //Write getter XD (pa no tener todo publico) Esta variable guarda la desviación (rotación) que debe tener el ojo


    public bool iCanSeeIt; // bool para saber si puede ver o no el target (si hay algun objeto interponiendo es false)
    float angleOfDisalignment; // guarda el ángulo al cual debe rotar
    public bool isSquint; // bool para saber si este ojo es el desviado
    bool isTropia; // para saber si es tropia (o es foria o es tropia)
    bool isForia; // para saber si es foria

    [SerializeField]GameObject pupil; // Referencia a la pupila del ojo.
    [SerializeField]EyeMovements fellowEye; // Referencia del otro ojo (usada para determinar el comportamiento)

    public EyeMovements FellowEye() { {return fellowEye;}}
    Quaternion fellowEyeRotation; // Referencia del Quaternion del otro ojo


    [SerializeField]float turningSpeed = 30f; //Velocidad de giro del ojo (Se cambia en el inspector)

    void Start()
    {
        eyeSettings = GameObject.Find("Eyes").GetComponent<EyeSettings>(); 
        target = GameObject.FindGameObjectWithTag("Target").transform;

        // Set Deviation. Se llama para inicializar los parámetros de cada ojo
        SetDeviation();
        Debug.Log(this.name + deviation + "Strabismus: " + isSquint);
    }

    void Update()
    {
        TropiaBehaviour();
        //ForiaBehaviour();
    }


    //Método para establecer la desviación en caso de que el ojo sea 
    void SetDeviation()
    {
        isForia = eyeSettings.isForia;
        isTropia = eyeSettings.isTropia;

        //deviation = transform.rotation;
        if(isSquint) 
        {
            if(isTropia)
            {
                angleOfDisalignment = eyeSettings.angleOfDisalignment;
                deviation = Quaternion.Euler(0 , angleOfDisalignment, 0);
            }
        }
    } 

    void TropiaBehaviour()
    {
         iCanSeeIt = HaveLineOfSightRayCast();
        if(isSquint)
        {
            if(iCanSeeIt && !fellowEye.iCanSeeIt) Turn(target);
            else
                transform.rotation = Quaternion.Slerp(transform.rotation, calculateGap() , Time.deltaTime * turningSpeed);
        }
        else
        {
            if(fellowEye.iCanSeeIt && !iCanSeeIt)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, calculateGap(fellowEye.deviation) , Time.deltaTime * turningSpeed);
            }
            else
            {
                Turn(target);
            }
            
        }

        /*
        iCanSeeIt = HaveLineOfSightRayCast();
        //Debug.Log("Line of Sight: " + ICanSeeIt);

        if(iCanSeeIt)
        {
            if(!isSquint || isSquint && !fellowEye.iCanSeeIt)
                Turn(target);
            else TurnToInitial();
        }
        else
        {
            if(isSquint)TurnToInitial();
        }*/
    }

    void ForiaBehaviour()
    {

    }

    void Turn(Transform target)
    {
        //Debug.Log("The target is: " + target);
        //Debug.Log(name + " Rotating to: " + obj.rotation);
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
    }

    Quaternion calculateGap()
    {
        float x = fellowEye.transform.rotation.eulerAngles.x + deviation.eulerAngles.x;
        float y = fellowEye.transform.rotation.eulerAngles.y + deviation.eulerAngles.y;
        
        Quaternion gap = Quaternion.Euler(x,y,0);
        return gap;
    }

    Quaternion calculateGap(Quaternion fellowDev)
    {
        float x = fellowEye.transform.rotation.eulerAngles.x - fellowDev.eulerAngles.x;
        float y = fellowEye.transform.rotation.eulerAngles.y - fellowDev.eulerAngles.y;
        
        Quaternion gap = Quaternion.Euler(x,y,0);
        return gap;
    }

    void TurnToInitial()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, deviation, Time.deltaTime * turningSpeed);
    }

    bool HaveLineOfSightRayCast()
    {
        RaycastHit hit;
        Vector3 direction = target.position - pupil.transform.position;
        
        if(Physics.Raycast(pupil.transform.position, direction, out hit, 50f))
        {

            if(hit.transform.CompareTag("Target"))
            {
                //Debug.Log("Hit: " + hit.GetType());
                Debug.DrawRay(pupil.transform.position, direction , Color.blue);
                //Debug.Log(hit.transform.eulerAngles);
                return true;
            }
        }
        return false;
    }
}
