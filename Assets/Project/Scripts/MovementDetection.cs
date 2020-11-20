using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    [SerializeField] GameObject eyeObserved;
    [SerializeField] CoverUncover coverUncover;
    EyeMovements eyeObservedScript;

    Vector3 anglesOpen; //eulerAngles when fellow eye can see the target
    Vector3 anglesClosed; //eulerAngles when fellow eye cannot see the target

    Vector3 difference;

    string strabismusType;
    float maxDifference = 0;

    void Start()
    {
        eyeObservedScript = eyeObserved.GetComponent<EyeMovements>();
    }
    // Update is called once per frame
    void Update()
    {
        if(eyeObservedScript.iCanSeeIt && !eyeObservedScript.FellowEye().iCanSeeIt)
            UpdateAnglesClosed();
        else if(eyeObservedScript.iCanSeeIt && eyeObservedScript.FellowEye().iCanSeeIt)
            UpdateAnglesOpen();

        //Debug.Log(maxDifference);
        //Debug.Log("x: " + difference.x);
        //Debug.Log("y: " + difference.y);
        //Debug.Log("z: " + difference.z);

        StrabismusType();
        
    }

    void UpdateAnglesOpen()
    {
        //Debug.Log("Angles Open");
        RaycastHit hit;
        Vector3 direction = eyeObserved.transform.position - transform.position;

        if(Physics.Raycast(transform.position, direction, out hit, 50f))
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            anglesOpen =  hit.transform.eulerAngles;
        }
    }
    
    void UpdateAnglesClosed()
    {
        //Debug.Log("Angles CLosed");
        RaycastHit hit;
        Vector3 direction = eyeObserved.transform.position - transform.position;

        if(Physics.Raycast(transform.position, direction, out hit, 50f))
        {
            Debug.DrawRay(transform.position, direction, Color.red);
            anglesClosed =  hit.transform.eulerAngles;
            CalculateDifference();
        }
    }

    void CalculateDifference()
    {
        float tmp = Mathf.Abs(anglesOpen.magnitude - anglesClosed.magnitude);
        if(maxDifference == 0)
        {
            maxDifference = tmp;
            difference = anglesOpen - anglesClosed;
        }
        else if(tmp > maxDifference && tmp - maxDifference < 2 * maxDifference) /// El segundo argumento es para filtrar repentinos cambios no deseados
        {
            maxDifference = tmp; // Si la magnitud del vector de diferencia es mayor a la máxima antes encontrada, se guarda como nuevo max.
            difference = anglesOpen - anglesClosed; // Se guarda el vector de diferencia.
        }
    }

    void StrabismusType()
    {
        if(maxDifference > 0)
        {
            if(difference.y > 0.5 && eyeObserved.name == "REye" || difference.y > 0.5 && eyeObserved.name == "LEye")
            {
                coverUncover.strabismusType = "Exo";
                coverUncover.eye = eyeObserved.name;
                Debug.Log(eyeObserved.name+": "+"Exo");
            }
            else if(difference.y < -0.5 && eyeObserved.name == "LEye" || difference.y < -0.5 && eyeObserved.name == "REye")
            {
                coverUncover.strabismusType = "Endo";
                coverUncover.eye = eyeObserved.name;
                Debug.Log(eyeObserved.name+": "+"Endo");
            }
        }
    }
}
