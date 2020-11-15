using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TempData : MonoBehaviour
{
    public float maxAngle = 25f;

    public float angleOfDisalignment;
    public bool endo;
    public bool exo;
    public bool isForia;
    public bool isTropia;

    public bool lefSquint;
    public bool rightSquint;

    [SerializeField] Dropdown chooseEye;
    [SerializeField] Dropdown type;
    [SerializeField] Slider angle;
    [SerializeField] Text angleValue;

    [SerializeField] GameObject save;
    
    void Update()
    {
        OnChangeEye(chooseEye.value);
        OnChangeType(type.value);
        OnAngleChange(angle.value);
    }

    public void OnChangeEye(int input)
    {
        
        if(input == 0 )
        {   
            lefSquint = true;
            rightSquint = false;
        }
        else if(input == 1){
            lefSquint = false;
            rightSquint = true;
        }
    }

    public void OnChangeType(int input)
    {
        
        if(input == 0)
        {   
            endo = true;
            exo = false;
        }
        else if(input == 1)
        {
            endo = false;
            exo = true;
        }
    }

    public void OnAngleChange(float input)
    {
        
        angleOfDisalignment = input; 
        angleValue.text = input.ToString() + " °";
    }

    public void GenRandomData()
    {
        
        float random = Random.value;
        if(random > 0.5) 
        {
            lefSquint = true;
            chooseEye.value = 0;
        }
        else 
        {
            rightSquint = true;
            chooseEye.value = 1;
        }


        /*
        isForia = false;
        isTropia = false;
        random = 0.51f; //////////////////////// MUST BE RANDOM.
        if(random > 0.5) isTropia = true;
        else isForia = true;*/

        
        random =Random.value; 
        if(random > 0.5) 
        {
            endo = true;
            type.value = 0;
        }
        else 
        {
            exo = true; 
            type.value = 1;
        }       

        angleOfDisalignment = Random.value * maxAngle;
        angle.value = angleOfDisalignment;
    }

    public void SaveData()
    {
        StrabismusData tmp = GameObject.Find("StrabismusData").GetComponent<StrabismusData>();
        tmp.lefSquint = this.lefSquint;
        tmp.rightSquint = this.rightSquint;
        tmp.endo = this.endo;
        tmp.exo = this.exo;
        tmp.isForia = false; //////*******FOR NOW ... maybe (otherwise it'll be removed)
        tmp.isTropia = true; //////*******FOR NOW ... maybe (otherwise it'll be removed)
        tmp.angleOfDisalignment = this.angleOfDisalignment;
        
        SceneLoader sl = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        sl.LoadScene("MainMenu");
    }
    
}
