using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PrefabInteractionHandler : MonoBehaviour
{
	//Used to store/load victim data from dictionary
	LynchingVictimPrefabManager prefabManager;
	//Shown when a user selects a prefab on the map page
	public GameObject infoCanvasPopup;

	public TextMesh markerName;
	public TextMeshProUGUI victimNameText;
	public Image victimImage;
	public TextMeshProUGUI locationText;
	public TextMeshProUGUI dateText;
	public Toggle markerOnSiteBool;
	public Button ARButton;
	public Button directionsButton;
	public Button infoButton;

	public void Start()
	{
		//Set the camera of the attached InfoCanvasPopup child to the main camera in the scene
		GameObject cameraGO = GameObject.Find("Main Camera");
		infoCanvasPopup.GetComponent<Canvas>().worldCamera = cameraGO.GetComponent<Camera>();

		//Get the LV Prefab Manager from our active scene with all the data loaded in
		prefabManager = GameObject.Find("Manager").GetComponent<LynchingVictimPrefabManager>();
	}
	private void OnMouseDown()
	{
		//Check the tag of the gameobject clicked and see if that victim exists in our dictionary
		if(gameObject.CompareTag("Ell Persons"))
		{
			Debug.Log("We clicked Ell Persons");
			Dictionary<string, LynchingVictim> localVictimData = prefabManager.getVictimData();

			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Ell Persons", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				victimNameText.text = "";
			}
			else
			{
				Debug.Log("Victim not found in the dictionary");
			}

		}
	}
}
