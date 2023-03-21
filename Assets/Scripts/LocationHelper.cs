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
    GpsCoord centerpoint=new GpsCoord(35.1991124817235f,-89.8685209172011f);
    
    float shelbyCountyDistanceKm;
    //35.1991124817235, -89.8685209172011
    
    //
    
    void Start()
    {
        Input.location.Start();    
    
        var extLimit = new GpsCoord(34.9950365675264f, -89.88439649661038f);  
        shelbyCountyDistanceKm=distanceInKmBetweenEarthCoordinates(centerpoint,extLimit);

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
    public bool IsWithinDistance(GpsCoord location, float distance){
        return (distanceInKmBetweenEarthCoordinates(Input.location.lastData,location)*0.0001f)<=distance;
    }
    public bool IsInMemphis(){

        return distanceInKmBetweenEarthCoordinates(Input.location.lastData,centerpoint)<=shelbyCountyDistanceKm;
    }
}
