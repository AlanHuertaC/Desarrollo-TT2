using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoverUncover : MonoBehaviour
{
    GameObject occluder;
    GameObject leftEye;
    GameObject rightEye;


     Vector3 leftFront;
    Vector3 leftBellow;
    Vector3 rightFront;
    Vector3 rightBellow;

    Vector3[] path;
    [SerializeField] float duration;
    [SerializeField] PathMode pathMode;
    [SerializeField] PathType pathType;
        
    bool play =false;
    void Start()
    {
        occluder = GameObject.Find("Occluder");
        leftEye = GameObject.Find("LEye");
        rightEye = GameObject.Find("REye");
        SetParades();
    }


    void SetParades()
    {
        leftFront = new Vector3(-1.3099f, 0.5f, 1);
        leftBellow = new Vector3 (-1.3099f, -1, 1);
        rightFront = new Vector3(1.3099f, 0.5f, 1);
        rightBellow = new Vector3 (1.3099f, -1, 1);

        path = new Vector3 [12];
        path[0] = leftFront;
        path[1] = leftBellow;
        path[2] = leftFront;
        path[3] = leftBellow;
        path[4] = leftFront;
        path[5] = rightBellow;
        path[6] = rightFront;
        path[7] = rightBellow;
        path[8] = rightFront;
        path[9] = rightBellow;
        path[10] = rightFront;
        path[11] = rightBellow;
    }


    public void PlayAnimation()
    {
        Transform occluderTransform = occluder.transform;
        occluderTransform.DOPath(path,duration,pathType,pathMode,10);
    }
    
    
}
