using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MapApi : MonoBehaviour
{

    public static void OpenMap(string address){
        Application.OpenURL("https://www.google.com/maps/dir/?api=1&query="+ UnityWebRequest.EscapeURL(address));
    }
}
