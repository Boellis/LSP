using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LynchingVictimPrefabManager : MonoBehaviour
{
    Dictionary<string, LynchingVictim> victimData = new Dictionary<string, LynchingVictim>();

    void Start()
    {
        //Add all of the lynching sites members to a list
        LynchingVictim victim1 = new LynchingVictim("Ell Persons", "Location2", "May 22, 1917", "<link_to_site>", true);
        victimData.Add("Ell Persons", victim1);

        LynchingVictim victim2 = new LynchingVictim("People's Grocery", "Mississippi Blvd & Walker Ave", "Mar 9, 1892", "<link_to_site>", true);
        victimData.Add("People's Grocery", victim2);

        LynchingVictim victim3 = new LynchingVictim("Jesse Lee Bond", "Arlington, TN", "Apr 28, 1939", "<link_to_site>", true);
        victimData.Add("Jesse Lee Bond", victim3);

        LynchingVictim victim4 = new LynchingVictim("Lee Walker", "Location4", "July 23, 1893", "<link_to_site>", true);
        victimData.Add("Lee Walker", victim4);

        LynchingVictim victim5 = new LynchingVictim("Unnamed Victim", "Second Market", "Jan 1, 1851", "<link_to_site>", true);
        victimData.Add("Unnamed Victim", victim5);
    }

    public Dictionary<string, LynchingVictim> getVictimData()
	{
        return victimData;

    }
}
