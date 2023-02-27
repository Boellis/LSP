using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOpenMaps : MonoBehaviour
{
    public string addr="35.162650, -89.878067";
    // Start is called before the first frame update
    void Start()
    {
        MapApi.OpenMap(addr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
