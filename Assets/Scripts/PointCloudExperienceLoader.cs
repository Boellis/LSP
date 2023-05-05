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

    public LocationHelper locationHelper;

    //public GpsCoord targetLocation; // The location to check the distance from
    public float distanceThreshold; // The distance threshold in kilometers

    // Start is called before the first frame update
    void Start()
    {
        //pointCloudPicker();
    }

	public void pointCloudPicker()
	{
        //Get the current victim that the user has selected in the home view
        currentVictim = PlayerPrefs.GetString("CurrentVictim");

        Debug.Log("Current Victim: " + currentVictim);

        if (currentVictim == "Ell Persons")
        {
            GpsCoord ellPersonsGPS = new GpsCoord(35.15986f, -89.88131f);

            //Check to see if we're near the marker we clicked on. If not, we should alert the user and maybe even send them back to the map screen.
            if (checkIfNearMarker(ellPersonsGPS))
			{
                EllPersons.SetActive(true);
                LeeWalker.SetActive(false);
                JeseeLeeBond.SetActive(false);
                UnnamedVictim.SetActive(false);
                PeoplesGrocery.SetActive(false);
            }
            else
			{
                Debug.Log("We've clicked the Ell Persons 3D experience on the marker, but are not standing near the inperson marker");
			}

        }
        else if (currentVictim == "Lee Walker")
        {
            GpsCoord leeGPS = new GpsCoord(35.159f, -90.04867f);

            //Check to see if we're near the marker we clicked on. If not, we should alert the user and maybe even send them back to the map screen.
            if (checkIfNearMarker(leeGPS))
            {
                EllPersons.SetActive(false);
                LeeWalker.SetActive(true);
                JeseeLeeBond.SetActive(false);
                UnnamedVictim.SetActive(false);
                PeoplesGrocery.SetActive(false);
            }
            else
            {
                Debug.Log("We've clicked the Lee Walker 3D experience on the marker, but are not standing near the inperson marker");
            }

        }
        else if (currentVictim == "Jesse Lee Bond")
        {
            GpsCoord jesseGPS = new GpsCoord(35.29641f, -89.66213f);

            //Check to see if we're near the marker we clicked on. If not, we should alert the user and maybe even send them back to the map screen.
            if (checkIfNearMarker(jesseGPS))
            {
                EllPersons.SetActive(false);
                LeeWalker.SetActive(false);
                JeseeLeeBond.SetActive(true);
                UnnamedVictim.SetActive(false);
                PeoplesGrocery.SetActive(false);
            }
            else
            {
                Debug.Log("We've clicked the Jesse Lee Bond 3D experience on the marker, but are not standing near the inperson marker");
            }

        }
        else if (currentVictim == "Unnamed Victim")
        {
            GpsCoord unnamedGPS = new GpsCoord(35.15243f, -90.0486f);

            //Check to see if we're near the marker we clicked on. If not, we should alert the user and maybe even send them back to the map screen.
            if (checkIfNearMarker(unnamedGPS))
            {
                EllPersons.SetActive(false);
                LeeWalker.SetActive(false);
                JeseeLeeBond.SetActive(false);
                UnnamedVictim.SetActive(true);
                PeoplesGrocery.SetActive(false);
            }
            else
            {
                Debug.Log("We've clicked the Unnamed Victim 3D experience on the marker, but are not standing near the inperson marker");
            }

        }
        else if (currentVictim == "People's Grocery")
        {
            GpsCoord peoplesGPS = new GpsCoord(35.1194f, -90.0386f);

            //Check to see if we're near the marker we clicked on. If not, we should alert the user and maybe even send them back to the map screen.
            if (checkIfNearMarker(peoplesGPS))
            {
                EllPersons.SetActive(false);
                LeeWalker.SetActive(false);
                JeseeLeeBond.SetActive(false);
                UnnamedVictim.SetActive(false);
                PeoplesGrocery.SetActive(true);
            }
            else
            {
                Debug.Log("We've clicked the Peoples Grocery 3D experience on the marker, but are not standing near the inperson marker");
            }

        }
    }

    public bool checkIfNearMarker(GpsCoord targetLocationCoord)//35.1495f, -90.0490f
    {
        Debug.Log("Are we near the marker?");
        distanceThreshold = 1.0f; // Example distance threshold in kilometers

        // Check if the user is within the specified distance of the target location
        bool isWithinDistance = locationHelper.IsWithinDistance(targetLocationCoord, distanceThreshold);

        if (isWithinDistance)
        {
            Debug.Log("User is within the specified distance.");
            Debug.Log("The user's current position is:");
            Debug.Log(locationHelper.getCurrentLocation());
            return true;
        }
        else
        {
            Debug.Log("User is not within the specified distance.");
            return false;
        }
    }

}
