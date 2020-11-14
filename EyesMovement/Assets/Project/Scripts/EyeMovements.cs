using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovements : MonoBehaviour
{
    Transform target;
    public Quaternion deviation; //Write getter


    public bool iCanSeeIt;
    float angleOfDisalignment;
    public bool isSquint;
    bool isTropia;
    bool isForia;


    EyeSettings eyeSettings;
    [SerializeField]GameObject pupil;
    [SerializeField]EyeMovements fellowEye;
    Quaternion fellowEyeRotation;


    [SerializeField]float turningSpeed = 30f;

    void Start()
    {
        eyeSettings = GameObject.Find("Eyes").GetComponent<EyeSettings>();
        target = GameObject.FindGameObjectWithTag("Target").transform;

        // Set Deviation
        SetDeviation();
        Debug.Log(this.name + deviation + "Strabismus: " + isSquint);
    }

    void Update()
    {
        TropiaBehaviour();
        //ForiaBehaviour();
    }

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
                //hitPosition = hit.transform.position;
                return true;
            }
        }
        return false;
    }
}
