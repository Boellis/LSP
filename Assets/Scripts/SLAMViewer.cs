using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using UnityEngine;

public class SLAMViewer : MonoBehaviour
{
    public string path;

   // public List<string> vals;
    public ShaderCall renderer;
    public float  limit = 1000000;
    public float scaleFactor = 1; 
    public enum Versions  
    {
    Def, V1,V2,V3,pts 
    }

    public Versions typeFile; 
    
    public IEnumerator render(string pth)
    {
       
        var str = File.ReadAllLines(path);

        var vects = new List<Vector3>();
        var cols =  new List<Color32>();
        int i = 0; 
        foreach (var s in str)
        {
            if (i == 0)
            {
                i++; 
                continue;
            }
            var vals = s.Split(' ');
            byte intensity = 0;
            
            switch (typeFile)
            {
                case Versions.Def:
                         
                    vects.Add(new Vector3(float.Parse(vals[7]),float.Parse(vals[8]),float.Parse(vals[9])));
                    intensity =   (byte)float.Parse(vals[3]); 
                    break;
                case Versions.V1:
                    vects.Add(new Vector3(float.Parse(vals[1]),float.Parse(vals[2]),float.Parse(vals[3])));
                    intensity =   (byte)float.Parse(vals[0]); 
                    break;
                case Versions.V2:
                    vects.Add(new Vector3(float.Parse(vals[0]),float.Parse(vals[1]),float.Parse(vals[2])));
                    intensity =   (byte)float.Parse(vals[3]); 
                    break;

                case Versions.V3:
                    vects.Add(new Vector3(float.Parse(vals[0].Substring(vals[0].Length-3)),float.Parse(vals[1].Substring(vals[1].Length-3)),float.Parse(vals[2])*10));
                    cols.Add(new Color32(byte.Parse(vals[4]),byte.Parse(vals[5]),byte.Parse(vals[6]),255));
                    break;
                case Versions.pts:
                    vects.Add(new Vector3(float.Parse(vals[0]),float.Parse(vals[1]),float.Parse(vals[2]))*scaleFactor);
                    cols.Add(new Color32(byte.Parse(vals[3]),byte.Parse(vals[4]),byte.Parse(vals[5]),255));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
          
               
          

          
           // cols.Add(new Color32(intensity,intensity,intensity,255));

            if (i > limit)
            {
                break;
            }
            i++; 
        }

        yield return new WaitForSeconds(5f); 
        Debug.Log(vects[34].ToString());
        renderer.setParticles(vects.ToArray(), cols.ToArray());
        
    }

    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine(render(Application.streamingAssetsPath + "/"+ path)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
