using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct GpsCoord{
    public float latitude;
    public float longitude;

    public GpsCoord(float latitude,float longitude){
        this.latitude=latitude;
        this.longitude=longitude;
    }
    public static implicit operator GpsCoord(LocationInfo l) => new GpsCoord(l.latitude,l.longitude);
}


public class LocationHelper : MonoBehaviour
{
    // Start is called before the first frame update
    GpsCoord centerpoint=new GpsCoord(35.123379f, -90.019434f);
    
    float shelbyCountyDistanceKm = 20;
    //35.1991124817235, -89.8685209172011




    void Start()
    {
        startLocationServices();
        //var extLimit = new GpsCoord(34.9950365675264f, -89.88439649661038f);  
        //shelbyCountyDistanceKm=distanceInKmBetweenEarthCoordinates(centerpoint,extLimit);

    }

    public void startLocationServices()
	{
        StartCoroutine(StartLocationService());

    }
    // check for the location service status and start it if necessary
    IEnumerator StartLocationService()
    {

        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location service is not enabled by user.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out while initializing location service.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            yield break;
        }
        else
		{
            // location services is turned on
		}
    }

    public GpsCoord getCurrentLocation()
	{
        return CurrentLocation;
    }

    public bool isReady => Input.location.status==LocationServiceStatus.Running;
    /// <summary>
    /// Returns the distance in kilometers between two GPS coordinates
    /// </summary>
    /// <param name="l1"></param>
    /// <param name="l2"></param>
    /// <returns></returns>
    float distanceInKmBetweenEarthCoordinates(GpsCoord l1, GpsCoord l2) {
        float earthRadiusKm = 6371;

        var dLat = Mathf.Deg2Rad*(l2.latitude-l1.latitude);
        var dLon = Mathf.Deg2Rad*(l2.longitude-l1.longitude);

        var lat1 = Mathf.Deg2Rad*(l1.latitude);
        var lat2 = Mathf.Deg2Rad*(l2.latitude);

        var a = Mathf.Sin(dLat/2) * Mathf.Sin(dLat/2) +
                Mathf.Sin(dLon/2) * Mathf.Sin(dLon/2) * Mathf.Cos(lat1) * Mathf.Cos(lat2); 
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1-a)); 
        return earthRadiusKm * c;
    }
    /*public bool IsWithinDistance(GpsCoord targetLocation, float distance){
        return (distanceInKmBetweenEarthCoordinates(getCurrentLocation(), targetLocation) * 0.0001f) <= distance;

        //return (distanceInKmBetweenEarthCoordinates(Input.location.lastData,location)*0.0001f)<=distance;
    }
    public bool IsInMemphis(){

        return distanceInKmBetweenEarthCoordinates(Input.location.lastData,centerpoint)<=shelbyCountyDistanceKm;
    }*/

    // get the current GPS location of the user,
    public GpsCoord CurrentLocation
    {
        get
        {
            if (isReady)
            {
                Debug.Log("Here is our current Location:");
                Debug.Log(Input.location.lastData);
                return Input.location.lastData;
            }
            else
            {
                Debug.Log("Location service is not ready.");
                return new GpsCoord(0, 0);
            }
        }
    }
    //-------------------------Extras added on by brandon-------------------------
    //public float memphisLat = 35.1495f;
    //public float memphisLong = -90.0490f;
    // public float distanceThreshold = 50.0f;  // Distance threshold in kilometers
    public bool IsWithinMemphis()
    {
        float userLatitude = Input.location.lastData.latitude;
        float userLongitude = Input.location.lastData.longitude;

        float distance = HaversineDistance(userLatitude, userLongitude, 35.1495f, -90.0490f);
        return distance <= 50.0f;

    }

    public float HaversineDistance(float lat1, float lon1, float lat2, float lon2)
    {
        float R = 6371; // Radius of the Earth in kilometers
        float diffLat = Mathf.Deg2Rad * (lat2 - lat1);
        float diffLon = Mathf.Deg2Rad * (lon2 - lon1);
        float a = Mathf.Sin(diffLat / 2) * Mathf.Sin(diffLat / 2) +
                   Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                   Mathf.Sin(diffLon / 2) * Mathf.Sin(diffLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return R * c; // Distance in kilometers
    }
}
