using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LynchingVictimPrefabManager : MonoBehaviour
{
    Dictionary<string, LynchingVictim> victimData = new Dictionary<string, LynchingVictim>();

    void Start()
    {
        //Add all of the lynching sites members to a list
        LynchingVictim victim1 = new LynchingVictim("Ell Persons", "Location2", "May 22, 1917", "Upon his capture by a mob local papers announced that he would be burned the next morning. The crowd gathered to watch was estimated at 3,000. Vendors set up stands among the crowd and sold sandwiches and snacks. It was reportedly a carnival-like atmosph","https://lynchingsitesmem.org/lynching/ell-persons", true);
        victimData.Add("Ell Persons", victim1);

        LynchingVictim victim2 = new LynchingVictim("People's Grocery", "Mississippi Blvd & Walker Ave", "Mar 9, 1892", "The three black grocers, all family men, were arrested and jailed. Three days later the downtown jail was stormed and Stewart, Moss and McDowell were dragged out and taken to the nearby Chesapeake and Ohio rail yards.", "https://lynchingsitesmem.org/lynching/peoples-grocery-lynchings-thomas-moss-will-stewart-calvin-mcdowell", true);
        victimData.Add("People's Grocery", victim2);

        LynchingVictim victim3 = new LynchingVictim("Jesse Lee Bond", "Arlington, TN", "Apr 28, 1939", "Jesse Lee Bond was lynched in Arlington,Tennessee on April 28, 1939 – in broad daylight, on the town square. The authorities lied about it, and the newspapers remained silent. It took more than seven decades, but Jesse Lee Bond's 96-year-old brother, Charle Morris, finally succeeded, through persistence and faith, in bringing this murder to light.", "https://lynchingsitesmem.org/lynching/jesse-lee-bond", true);
        victimData.Add("Jesse Lee Bond", victim3);

        LynchingVictim victim4 = new LynchingVictim("Lee Walker", "Lee Walker Memorial", "July 23, 1893", "That night, a mob stormed the jail and Walker was dragged out and hanged and burned. Because the lynching took place in downtown Memphis, the city newspapers were able to cover the lynching in shocking and lurid detail.", "https://lynchingsitesmem.org/lynching/lee-walker", true);
        victimData.Add("Lee Walker", victim4);

        LynchingVictim victim5 = new LynchingVictim("Unnamed Victim", "Second Market", "Jan 1, 1851", "Downtown Memphis near Market and 2nd", "https://lynchingsitesmem.org/lynching/name-unknown-downtown-memphis-1851", true);
        victimData.Add("Unnamed Victim", victim5);
    }

    public Dictionary<string, LynchingVictim> getVictimData()
	{
        return victimData;

    }
}
