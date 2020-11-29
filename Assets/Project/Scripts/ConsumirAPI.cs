using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Net;


public class ConsumirAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private static void GetItems()
    {
        var url = $"http://localhost/API";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse()) 
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        // Do something with responseBody
                        Debug.Log("Devuelve " + responseBody);
                        Api api = new Api();
                        api = JsonUtility.FromJson<Api>(responseBody);
                        Debug.Log(api.idPaciente);


                    }
                }
            }
        }
        catch (WebException ex)
        {
            // Handle error
        }
    }
}

public class Api
{
    public string id;
    public string idPaciente;
    public string code;
    public string statusMessage;
}

