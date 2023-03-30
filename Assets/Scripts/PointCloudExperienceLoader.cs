using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudExperienceLoader: MonoBehaviour
{
    string currentVictim;

    public GameObject EllPersons;
    public GameObject LeeWalker;
    public GameObject JeseeLeeBond;
    public GameObject UnnamedVictim;
    public GameObject PeoplesGrocery;

    // Start is called before the first frame update
    void Start()
    {
        //Get the current victim that the user has selected in the home view
        currentVictim = PlayerPrefs.GetString("CurrentVictim");

        if (currentVictim == "Ell Persons")
		{
            EllPersons.SetActive(true);

        }
        else if (currentVictim == "Lee Walker")
		{
            LeeWalker.SetActive(true);

        }
        else if (currentVictim == "Jesse Lee Bond")
        {
            JeseeLeeBond.SetActive(true);

        }
        else if (currentVictim == "Unnamed Victim")
        {
            UnnamedVictim.SetActive(true);

        }
        else if (currentVictim == "People's Grocery")
        {
            PeoplesGrocery.SetActive(true);

        }
    }

}
