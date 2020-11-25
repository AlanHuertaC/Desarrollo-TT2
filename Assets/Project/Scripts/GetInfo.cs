using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetInfo : MonoBehaviour
{
    [SerializeField] Text angleValueR;
    [SerializeField] Text angleValueL;
    [SerializeField] Text typeOfDeviation;
    [SerializeField] Text diopter;

    GameObject resultsObject;
    string strabismusType;
    string eye;

    string angleOfDisalignment;
    string prismas;
    Results results;
    // Start is called before the first frame update
    void Start()
    {
        resultsObject = GameObject.Find("Results(Clone)");
        GetData();
        
    }

    void Update()
    {
        PlaceData();
    }
    void GetData()
    {
        if(resultsObject != null)
        {
            results = resultsObject.GetComponent<Results>();
            strabismusType = results.strabismusType;
            eye = results.eye;
            angleOfDisalignment = results.angleOfDisalignment.ToString("0.00");
            prismas = (results.angleOfDisalignment * 1.75).ToString("0.00");
            Debug.Log(angleOfDisalignment);
        }
    }
    void PlaceData()
    {
        if(resultsObject != null)
        {
            typeOfDeviation.text ="Tipo de estrabismo: " + strabismusType + "tropía";
            diopter.text = "Dioptrías prismáticas: "+ prismas;
            if(results.eye == "REye")
            {
                angleValueL.text = "Ángulo de desviación Ojo Izquierdo: 0°";
                angleValueR.text = "Ángulo de desviación Ojo Derecho: " + angleOfDisalignment+"°"; 
            }
            else
            {
                angleValueR.text = "Ángulo de desviación Ojo Derecho: 0°";
                angleValueL.text = "Ángulo de desviación Ojo Izquierdo: " + angleOfDisalignment +"°"; 
            }
        }
        else
        {
            Debug.Log("nullllll");
        }
    }

    
}
