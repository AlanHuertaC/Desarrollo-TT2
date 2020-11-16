using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableScenes : MonoBehaviour
{
    Button setStrabismus;
    Button coverUncover;
    Button measureDev;
    Button results;

    StrabismusData strabismusData;
    bool enableTest = false;
    bool enableResult = false;
    
    void Start()
    {
        setStrabismus = GameObject.Find("SetStrabismus").GetComponent<Button>();
        coverUncover = GameObject.Find("CoverUncover").GetComponent<Button>();
        measureDev= GameObject.Find("MeasureDeviation").GetComponent<Button>();
        results = GameObject.Find("Results").GetComponent<Button>();
        
        coverUncover.interactable = false;
        measureDev.interactable = false;
        results.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        EnableTestsResults();
    }

    void CheckStatus()
    {
        if(GameObject.Find("StrabismusData") != null) enableTest = true;
        if(GameObject.Find("ResultsData") != null) enableResult = true; 
    }

    void EnableTestsResults()
    {
        if(enableTest)
        {
            coverUncover.interactable = true;
            measureDev.interactable = true;
        }
        if(enableResult)
            results.interactable = true;
    }
}
