using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
    Clase utilizada en la escena "SetStrabismus" para obtener los datos de la interfaz de usuario.

    Al presionar "guardar", los datos ingresados por el usuario se pasan a la clase StrabismusData (encargada
    de compartir estos datos entre escenas).

*/

public class TempData : MonoBehaviour
{
    public float maxAngle = 25f;

    public float angleOfDisalignment;
    public string typeOfDeviation;
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
            typeOfDeviation = "endo";
        }
        else if(input == 1)
        {
            typeOfDeviation = "exo";
        }
        else if(input == 2)
        {
            typeOfDeviation = "hiper";
        }
        else
        {
            typeOfDeviation = "hipo";
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
            typeOfDeviation = "endo";
            type.value = 0;
        }
        else 
        {
            typeOfDeviation = "exo";
            type.value = 1;
        }       

        angleOfDisalignment = Random.value * maxAngle;
        angle.value = angleOfDisalignment;
    }

    public void SaveData()
    {
        StrabismusData data = GameObject.Find("StrabismusData").GetComponent<StrabismusData>();
        data.lefSquint = this.lefSquint;
        data.rightSquint = this.rightSquint;
        data.typeOfDeviation = typeOfDeviation;
        data.isForia = false; //////*******FOR NOW ... maybe (otherwise it'll be removed)
        data.isTropia = true; //////*******FOR NOW ... maybe (otherwise it'll be removed)
        data.angleOfDisalignment = this.angleOfDisalignment;
        
        SceneLoader sl = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        sl.LoadScene("MainMenu");
    }
    
}
