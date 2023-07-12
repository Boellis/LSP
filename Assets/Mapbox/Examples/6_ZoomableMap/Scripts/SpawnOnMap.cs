namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using System.Collections;
	using System;

	public class SpawnOnMap : MonoBehaviour
	{ //HumanoidPointCloudShader
		[SerializeField]
		public AbstractMap _map;

		[SerializeField]
		[Geocode]
		public string[] _locationStrings;
		public Vector2d[] _locations;

		[SerializeField]
		public float _spawnScale = 100f;

		[SerializeField]
		public GameObject _markerPrefab;

		public List<GameObject> _spawnedObjects;

		public GameObject userOutOfRangePopup;

		public GameObject enableLocationServicesPopupIOS;
		public GameObject enableLocationServicesPopupAndroid;


		//public LocationHelper locationHelper;
		public GameObject youAreHereMarkerPrefab;
		public List<string> lynchingSiteTags = new List<string>();

		void Start()
		{
			// Addtional code used to set tags for markers so data can be properly assigned to them
			lynchingSiteTags.Add("Ell Persons");
			lynchingSiteTags.Add("People's Grocery");
			lynchingSiteTags.Add("Jesse Lee Bond");
			lynchingSiteTags.Add("Lee Walker");
			lynchingSiteTags.Add("Unnamed Victim");
			lynchingSiteTags.Add("Player");


			StartCoroutine(StartLocationServiceAndSpawnMarkers());
		}

		//Turn the out of range popup on and off after a few seconds
		public IEnumerator showOutOfRangePopup(float waitSeconds)
		{
			userOutOfRangePopup.SetActive(true);
			yield return new WaitForSeconds(waitSeconds);
			userOutOfRangePopup.SetActive(false);
		}

		public IEnumerator showEnableLocationServicesPopup(GameObject popup)
		{
			popup.SetActive(true);
			yield return new WaitForSeconds(120f);
			popup.SetActive(false);
		}

		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}

		//---------------------Location---------------------
		IEnumerator StartLocationServiceAndSpawnMarkers()
		{

			if (!Input.location.isEnabledByUser)
			{
				//StartCoroutine(showEnableLocationServicesPopup(enableLocationServicesPopupAndroid));

				Debug.Log("Location service is not enabled by user.");

#if UNITY_ANDROID
					StartCoroutine(showEnableLocationServicesPopup(enableLocationServicesPopupAndroid));

#elif UNITY_IOS
				StartCoroutine(showEnableLocationServicesPopup(enableLocationServicesPopupIOS));

#endif
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
				if (IsWithinMemphis())//only handle markers if user is out of memphis
				{
					//Create new array with extra slot
					string[] isInMemphisArray = new string[_locationStrings.Length + 1];

					// Copy the old array into the new one
					Array.Copy(_locationStrings, isInMemphisArray, _locationStrings.Length);

					// Add the new item
					//isInMemphisArray[isInMemphisArray.Length - 1] = "35.123379, -90.019434";
					isInMemphisArray[isInMemphisArray.Length - 1] = Input.location.lastData.latitude.ToString() + "," + Input.location.lastData.longitude.ToString();

					//Set our old array to our updated array with our users current location
					_locationStrings = isInMemphisArray;

					//_locationStrings[5] = Input.location.lastData.latitude.ToString() + "," + Input.location.lastData.longitude.ToString();
					_locations = new Vector2d[_locationStrings.Length];
					_spawnedObjects = new List<GameObject>();
					for (int i = 0; i < _locationStrings.Length; i++)
					{
						var locationString = _locationStrings[i];
						_locations[i] = Conversions.StringToLatLon(locationString);
						GameObject instance;
						//Spawn the normal marker on the map
						if (i != 5)
						{
							instance = Instantiate(_markerPrefab);
						}
						//If we're in memphis, we'll have 6 locations so spawn the you are here marker now
						else
						{
							instance = Instantiate(youAreHereMarkerPrefab);
						}
						//Make sure that the order you add in the location strings match the order of the lynching site tags or you will have the wrong info being loaded in
						//Set the tag of the prefab
						instance.tag = lynchingSiteTags[i];
						Debug.Log("Site tag:");
						Debug.Log(lynchingSiteTags[i]);

						instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
						instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
						_spawnedObjects.Add(instance);

						
					}
				}
				else //handle both user location marker and lsp site markers
				{
					//Show the popup for 3 seconds that we're not in Memphis.
					StartCoroutine(showOutOfRangePopup(5f));
					_locations = new Vector2d[_locationStrings.Length + 1];//the +1 is to account for the user's location + marker
					_spawnedObjects = new List<GameObject>();
					for (int i = 0; i < _locationStrings.Length; i++)
					{
						var locationString = _locationStrings[i];
						_locations[i] = Conversions.StringToLatLon(locationString);
						var instance = Instantiate(_markerPrefab);

						//Make sure that the order you add in the location strings match the order of the lynching site tags or you will have the wrong info being loaded in
						//Set the tag of the prefab
						instance.tag = lynchingSiteTags[i];
						Debug.Log("Site tag:");
						Debug.Log(lynchingSiteTags[i]);

						instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
						instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
						_spawnedObjects.Add(instance);
					}
				}
			}
			
			Input.location.Stop();


		}

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
}