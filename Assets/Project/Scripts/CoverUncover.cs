using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/*
    Esta clase es la encargada de reproducir la animación del cover test.

*/


public class CoverUncover : MonoBehaviour
{
    GameObject occluder;
    GameObject leftEye;
    GameObject rightEye;

    EyeMovements left;
    EyeMovements right;


     Vector3 leftFront;
    Vector3 leftBellow;
    Vector3 rightFront;
    Vector3 rightBellow;

    Vector3[] path;
    [SerializeField] float duration;
    //[SerializeField] PathMode pathMode;
    //[SerializeField] PathType pathType;

    [SerializeField] Ease moveEase = Ease.Linear;

    [SerializeField] GameObject rightDetector;
    [SerializeField] GameObject leftDetector;
    [SerializeField] GameObject results;

        
    public string strabismusType;
    public string eye;

    bool play =false;


    Tween tween;
    void Start()
    {
        occluder = GameObject.Find("Occluder");
        leftEye = GameObject.Find("LEye");
        rightEye = GameObject.Find("REye");
        left = leftEye.GetComponent<EyeMovements>();
        right = rightEye.GetComponent<EyeMovements>();

        leftDetector.SetActive(false);
        rightDetector.SetActive(true);
        
        SetParades();
    }


    void SetParades()
    {
        leftFront = new Vector3(-1.3099f, 0.5f, 1);
        leftBellow = new Vector3 (-1.3099f, -1, 1);
        rightFront = new Vector3(1.3099f, 0.5f, 1);
        rightBellow = new Vector3 (1.3099f, -1, 1);

        path = new Vector3 [13];
        path[0] = leftFront;
        path[1] = leftBellow;
        path[2] = leftFront;
        path[3] = leftBellow;
        path[4] = leftFront;
        path[5] = leftBellow;
        path[6] = rightBellow;
        path[7] = rightFront;
        path[8] = rightBellow;
        path[9] = rightFront;
        path[10] = rightBellow;
        path[11] = rightFront;
        path[12] = rightBellow;
    }


    public void PlayAnimation()
    {
        play = true;
        Transform occluderTransform = occluder.transform;
        //tween = occluderTransform.DOPath(path,duration,pathType,pathMode,10);
        tween = occluderTransform.DOPath(path,duration).SetEase(moveEase).OnComplete(GenerateResult);

    }

    void Update()
    {
        EnableDetectors();
    }

    void EnableDetectors()
    {
        if(play)
        {
            if(tween.ElapsedPercentage() > 0.5)
            {
                leftDetector.SetActive(true);
                rightDetector.SetActive(false);
            }
            else
            {
                leftDetector.SetActive(false);
                rightDetector.SetActive(true);
            }
        }
    }

    void GenerateResult()
    {
        Debug.Log("Done!");
        Debug.Log(strabismusType);
        if(GameObject.Find("Results") == null)
        {
            Instantiate(results);
        }
        results =   GameObject.Find("Results(Clone)");

        Results data = results.GetComponent<Results>();
        data.strabismusType = strabismusType;
        data.eye = eye;
    }



    


    
}
