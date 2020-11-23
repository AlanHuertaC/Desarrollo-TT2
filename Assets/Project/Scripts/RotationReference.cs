using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationReference : MonoBehaviour
{
   Transform target;
   int layer = 8;
   int layerMask;

   [SerializeField]float turningSpeed = 60f; 

   void Start()
   {
       target = GameObject.FindGameObjectWithTag("Target").transform;
       layerMask = 1 << layer;
   }

   void Update()
   {
       if(HaveLineOfSightRayCast())
       {
           Turn(target);
       }
   }

   bool HaveLineOfSightRayCast()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        
        if(Physics.Raycast(transform.position, direction, out hit, 500f, layerMask))
        {

            if(hit.transform.CompareTag("Target"))
            {
                //Debug.Log("Hit: " + hit.GetType());
                Debug.DrawRay(transform.position, direction , Color.yellow);
                //Debug.Log(hit.transform.eulerAngles);
                return true;
            }
        }
        return false;
    }

    void Turn(Transform target)
    {
        //Debug.Log("The target is: " + target);
        //Debug.Log(name + " Rotating to: " + obj.rotation);
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
    }


}
