using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MapApi : MonoBehaviour
{

    public static void OpenMap(string address){
        var escAddr=UnityWebRequest.EscapeURL(address);
        //Debug.Log(escAddr);
        Application.OpenURL("https://www.google.com/maps/dir/?api=1&destination="+ escAddr);
    }
}
