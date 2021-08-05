using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
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
     Vector3 leftOn;
    Vector3 leftOff;
    Vector3 rightOn;
    Vector3 rightOff;

    Vector3[] path;
    Vector3[] path2;
    
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
        leftOn = new Vector3(-1.95f, -0.34f, 1.35f);
        leftOff = new Vector3 (-2.51f, -1.2f, 1.35f);
        rightOn = new Vector3(1.95f, -0.34f, 1.35f);
        rightOff = new Vector3 (2.51f, -1.2f, 1.35f);

        path = new Vector3 [6];
        path2 = new Vector3 [6];

        path[0] = leftOn;
        path[1] = leftOff;
        path[2] = leftOn;
        path[3] = leftOff;
        path[4] = leftOn;
        path[5] = leftOff;

        path2[0] = rightOn;
        path2[1] = rightOff;
        path2[2] = rightOn;
        path2[3] = rightOff;
        path2[4] = rightOn;
        path2[5] = rightOff;
    }


    public void PlayAnimation()
    {
        play = true;
        Transform occluderTransform = occluder.transform;
        //tween = occluderTransform.DOPath(path,duration,pathType,pathMode,10);
        //tween = occluderTransform.DOPath(path,duration).SetEase(moveEase).OnComplete(GenerateResult);
        tween = DOTween.Sequence()
            .Append(occluderTransform.DOPath(path,duration).SetEase(moveEase).OnComplete(DisableCollider))
            .Append(occluderTransform.DOMove(new Vector3(0.79f,-0.46f,3.44f), 2f))
            .Append(occluderTransform.DORotate(new Vector3(0,0,33f),1f).OnComplete(EnableCollider))
            .Append(occluderTransform.DOPath(path2,duration).SetEase(moveEase).OnComplete(GenerateResult));

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

    void DisableCollider()
    {
        Collider[] cs = occluder.GetComponentsInChildren<Collider>();
        foreach(Collider c in cs)
        {
            c.enabled = false;
        }
    }
    void EnableCollider()
    {
        Collider[] cs = occluder.GetComponentsInChildren<Collider>();
        foreach(Collider c in cs)
        {
            c.enabled = true;
        }
    }

    void GenerateResult()
    {
        Button measure = GameObject.Find("MedirDesviacion").GetComponent<Button>();
        measure.interactable = true;
        
        occluder.transform.position = leftOff;
        occluder.transform.Rotate(new Vector3(0,0,-66f));
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
        if(strabismusType == "endo" && eye == "REye" || strabismusType == "exo" && eye == "LEye" )
            data.direction = "right";
        else if(strabismusType == "exo" && eye == "REye" || strabismusType == "endo" && eye == "LEye")
            data.direction = "left";
        else if(strabismusType == "hipo")
            data.direction = "up";
        else //hiper
            data.direction = "down";
    }



    


    
}
