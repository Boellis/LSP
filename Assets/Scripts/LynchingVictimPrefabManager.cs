using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LynchingVictimPrefabManager : MonoBehaviour
{
    public List<LynchingVictim> cache;
    Dictionary<string, LynchingVictim> victimData = new Dictionary<string, LynchingVictim>();
    LocationHelper lh;
    void Start()
    {
        //Add all of the lynching sites members to a list
        /* LynchingVictim victim1 = new LynchingVictim("Ell Persons", "Wolf River Area Near Bartlett Rd.", "May 22, 1917", "Upon his capture by a mob local papers announced that he would be burned the next morning. The crowd gathered to watch was estimated at 3,000. Vendors set up stands among the crowd and sold sandwiches and snacks. It was reportedly a carnival-like atmosph","https://lynchingsitesmem.org/lynching/ell-persons", true);
        victimData.Add("Ell Persons", victim1);*/

        foreach(LynchingVictim v in cache){
            victimData.Add(v.name,v);
        }
    }

    

    public Dictionary<string, LynchingVictim> getVictimData()
	{
        return victimData;

    }
    ///Returns closest victim to current GPS coordinates or null if none.
    public LynchingVictim FindClosestMarker(){
        LynchingVictim lv=null;
        float distance=float.MaxValue;
        foreach(string key in victimData.Keys){
            var v = victimData[key];
            float d;
            if(lh.IsWithinDistance(v.coordinates,out d)){
                if(d<distance)
                    lv=v;
            }
            
        }
        return lv;
    }
}
