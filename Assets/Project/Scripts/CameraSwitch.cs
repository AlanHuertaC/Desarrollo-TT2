using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    GameObject MainCamera;
    //GameObject LeftEyeCam;
    //GameObject RightEyeCam;
    GameObject BothEyes;


    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("MainCamera");
        //LeftEyeCam = GameObject.Find("LeftEyeCam");
        //RightEyeCam = GameObject.Find("RighEyeCam");
        BothEyes = GameObject.Find("InfrontCam");
        MainCamera.SetActive(true);
        BothEyes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("SwitchCam"))
        {
            if(MainCamera.activeSelf)
            {
                MainCamera.SetActive(false);
                BothEyes.SetActive(true);
            }
            else
            {
                MainCamera.SetActive(true);
                BothEyes.SetActive(false);
            }
        }
    }
}
