namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using System.Collections;

	public class SpawnOnMap : MonoBehaviour
	{ //HumanoidPointCloudShader
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		string[] _locationStrings;
		Vector2d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

		public GameObject userOutOfRangePopup;

		public LocationHelper locationHelper;
		public GameObject youAreHereMarkerPrefab;
		void Start()
		{
			List<string> lynchingSiteTags = new List<string>();

			// Addtional code used to set tags for markers so data can be properly assigned to them
			lynchingSiteTags.Add("Ell Persons");
			lynchingSiteTags.Add("People's Grocery");
			lynchingSiteTags.Add("Jesse Lee Bond");
			lynchingSiteTags.Add("Lee Walker");
			lynchingSiteTags.Add("Unnamed Victim");

			if (!locationHelper.IsInMemphis())//only hand markers is user is out of memphis
			{
				_locations = new Vector2d[_locationStrings.Length]; 
			}
			else //handle both user location marker and lsp site markers
			{
				_locations = new Vector2d[_locationStrings.Length + 1];//the +1 is to account for the user's location + marker
			}

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

			//After we've spawned all the lynching site points on the map, spawn a marker where the user is located.
			//If we not the user is not in memphis, show them a popup encouraging them to visit Memphis.
			if(!locationHelper.IsInMemphis())
			{
				//Show the popup for 3 seconds that we're not in Memphis.
				StartCoroutine(showOutOfRangePopup(3f));
			}
			else //We are in Memphis
			{
				//We are in memphis so show the marker on the map
				Vector2d currentPosition = new Vector2d(locationHelper.getCurrentLocation().latitude, locationHelper.getCurrentLocation().longitude);
				//Show the marker on the map
				var instance = Instantiate(youAreHereMarkerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(currentPosition, true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
				_locations[5] = (currentPosition);
			}
		}

		//Turn the out of range popup on and off after a few seconds
		public IEnumerator showOutOfRangePopup(float waitSeconds)
		{
			userOutOfRangePopup.SetActive(true);
			yield return new WaitForSeconds(waitSeconds);
			userOutOfRangePopup.SetActive(false);
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
	}
}