using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LocationTest : MonoBehaviour
{
    public TextMeshProUGUI currentLocationText;
    public TextMeshProUGUI locationServiceStatus;
    public TextMeshProUGUI locationEnabledByUser;
    public TextMeshProUGUI isInMemphisText;

    public LocationHelper locationHelper;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {

        locationEnabledByUser.text = Input.location.isEnabledByUser.ToString();
       

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        locationServiceStatus.text = Input.location.status.ToString();

        if (maxWait < 1)
        {
            Debug.Log("Timed out while initializing location service.");
            locationServiceStatus.text = Input.location.status.ToString();
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            locationServiceStatus.text = Input.location.status.ToString();
            yield break;
        }
        else
		{
            string location = Input.location.lastData.latitude.ToString() + "," + Input.location.lastData.longitude.ToString();
            currentLocationText.text = location;

            isInMemphisText.text = locationHelper.IsWithinMemphis().ToString();
        }
    }
}
