using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PrefabInteractionHandler : MonoBehaviour
{
	//Used to store/load victim data from dictionary
	LynchingVictimPrefabManager prefabManager;
	//Local data that is being used by the prefab manager
	Dictionary<string, LynchingVictim> localVictimData;

	//Shown when a user selects a prefab on the map page
	public GameObject infoCanvasPopup;

	public TextMesh markerName;
	public TextMeshProUGUI victimNameText;
	public Image victimImage;
	public TextMeshProUGUI locationText;
	public TextMeshProUGUI dateText;
	public TextMeshProUGUI descriptionText;
	public Toggle markerOnSiteBool;
	public Button ARButton;
	public Button directionsButton;
	public Button infoButton;
	public GameObject OutOfRangeMarker;

	public void Start()
	{
		//Set the camera of the attached InfoCanvasPopup child to the main camera in the scene
		GameObject cameraGO = GameObject.Find("Main Camera");
		infoCanvasPopup.GetComponent<Canvas>().worldCamera = cameraGO.GetComponent<Camera>();

		//Get the LV Prefab Manager from our active scene
		prefabManager = GameObject.Find("Manager").GetComponent<LynchingVictimPrefabManager>();

		//Load the data from our LynchingVictimPrefabManager
		Dictionary<string, LynchingVictim> localVictimData = prefabManager.getVictimData();

		//-------------------------------

		//Check the tag of the object the script is attached to and set the name of the marker based on the associated tag
		if (this.gameObject.CompareTag("Ell Persons"))
		{
			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Ell Persons", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				//Set the name of the lynching site marker on the map
				markerName.text = victim.name;

			}
			else { Debug.Log("Victim not found in the dictionary"); }
		}
		//------------------------------------------------------------------

		//-------------------------------

		//Check the tag of the object the script is attached to and set the name of the marker based on the associated tag
		if (this.gameObject.CompareTag("People's Grocery"))
		{
			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("People's Grocery", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				//Set the name of the lynching site marker on the map
				markerName.text = victim.name;

			}
			else { Debug.Log("Victim not found in the dictionary"); }
		}
		//------------------------------------------------------------------

		//-------------------------------

		//Check the tag of the object the script is attached to and set the name of the marker based on the associated tag
		if (this.gameObject.CompareTag("Jesse Lee Bond"))
		{
			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Jesse Lee Bond", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				//Set the name of the lynching site marker on the map
				markerName.text = victim.name;

			}
			else { Debug.Log("Victim not found in the dictionary"); }
		}
		//------------------------------------------------------------------

		//-------------------------------

		//Check the tag of the object the script is attached to and set the name of the marker based on the associated tag
		if (this.gameObject.CompareTag("Lee Walker"))
		{
			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Lee Walker", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				//Set the name of the lynching site marker on the map
				markerName.text = victim.name;

			}
			else { Debug.Log("Victim not found in the dictionary"); }
		}
		//------------------------------------------------------------------

		//Check the tag of the object the script is attached to and set the name of the marker based on the associated tag
		if (this.gameObject.CompareTag("Unnamed Victim"))
		{
			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Unnamed Victim", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				//Set the name of the lynching site marker on the map
				markerName.text = victim.name;

			}
			else { Debug.Log("Victim not found in the dictionary"); }
		}
		//------------------------------------------------------------------
	}
	//The user has clicked/tapped on one of our prefab markers
	private void OnMouseDown()
	{
		//Load the data from our LynchingVictimPrefabManager
		Dictionary<string, LynchingVictim> localVictimData = prefabManager.getVictimData();
		//-------------------------------
		//Check the tag of the gameobject clicked and see if that victim exists in our dictionary
		if (gameObject.CompareTag("Ell Persons"))
		{
			Debug.Log("We clicked Ell Persons");

			//Turn on the prefab InfoCanvasPopup to show information about the site
			if (!infoCanvasPopup.activeInHierarchy)
			{
				infoCanvasPopup.SetActive(true);
			}
			//If the popup is already showing, turn it off
			else
			{
				infoCanvasPopup.SetActive(false);
			}
			
			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Ell Persons", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				victimNameText.text = victim.name;
				locationText.text = victim.location;
				dateText.text = victim.date;
				descriptionText.text = victim.description;

				infoButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage(victim.linkToSite); });
				ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/AR"); });
				//This function will be replaced this Jonathans Code to take them to directions
				directionsButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage("https://www.google.com/maps/dir/35.1158711,-90.0279878/35.159867,+-89.881309/@35.1598438,-89.9513502,12z/data=!4m7!4m6!1m1!4e1!1m3!2m2!1d-89.881309!2d35.159867"); });

				//Set the global variable for which victim wer're on. We'll use this to decide with 3D scene to display
				PlayerPrefs.SetString("CurrentVictim", "Ell Persons");


			}else{Debug.Log("Victim not found in the dictionary");}

		}

		//------------------------------------------------------------------------------------------

		//-------------------------------
		//Check the tag of the gameobject clicked and see if that victim exists in our dictionary
		if (gameObject.CompareTag("People's Grocery"))
		{
			Debug.Log("We clicked People's Grocery");

			//Turn on the prefab InfoCanvasPopup to show information about the site
			if (!infoCanvasPopup.activeInHierarchy)
			{
				infoCanvasPopup.SetActive(true);
			}
			//If the popup is already showing, turn it off
			else
			{
				infoCanvasPopup.SetActive(false);
			}

			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("People's Grocery", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				victimNameText.text = victim.name;
				locationText.text = victim.location;
				dateText.text = victim.date;
				descriptionText.text = victim.description;

				infoButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage(victim.linkToSite); });
				//Uncomment me when victim data is linked to specfic point cloud expereinces
				ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/AR"); });

				//Show the "not near marker" popup
				//ARButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(cycleMarkerOutOfRange()); });

				//This function will be replaced this Jonathans Code to take them to directions
				directionsButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage("https://www.google.com/maps/dir//35.119395,%20-90.038595"); });
				//Set the global variable for which victim wer're on. We'll use this to decide with 3D scene to display
				PlayerPrefs.SetString("CurrentVictim", "People's Grocery");
			}
			else { Debug.Log("Victim not found in the dictionary"); }

		}

		//------------------------------------------------------------------------------------------

		//-------------------------------
		//Check the tag of the gameobject clicked and see if that victim exists in our dictionary
		if (gameObject.CompareTag("Jesse Lee Bond"))
		{
			Debug.Log("We clicked Jesse Lee Bond");

			//Turn on the prefab InfoCanvasPopup to show information about the site
			if (!infoCanvasPopup.activeInHierarchy)
			{
				infoCanvasPopup.SetActive(true);
			}
			//If the popup is already showing, turn it off
			else
			{
				infoCanvasPopup.SetActive(false);
			}

			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Jesse Lee Bond", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				victimNameText.text = victim.name;
				locationText.text = victim.location;
				dateText.text = victim.date;
				descriptionText.text = victim.description;
				//Uncomment me when victim data is linked to specfic point cloud expereinces
				ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/AR"); });

				//Show the "not near marker" popup
				//ARButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(cycleMarkerOutOfRange()); });

				infoButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage(victim.linkToSite); });
				
				//This function will be replaced this Jonathans Code to take them to directions
				directionsButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage("https://www.google.com/maps/dir//35.296408,%20-89.662129"); });
				//Set the global variable for which victim wer're on. We'll use this to decide with 3D scene to display
				PlayerPrefs.SetString("CurrentVictim", "Jesse Lee Bond");
			}
			else { Debug.Log("Victim not found in the dictionary"); }

		}

		//------------------------------------------------------------------------------------------

		//-------------------------------
		//Check the tag of the gameobject clicked and see if that victim exists in our dictionary
		if (gameObject.CompareTag("Lee Walker"))
		{
			Debug.Log("We clicked Lee Walker");

			//Turn on the prefab InfoCanvasPopup to show information about the site
			if (!infoCanvasPopup.activeInHierarchy)
			{
				infoCanvasPopup.SetActive(true);
			}
			//If the popup is already showing, turn it off
			else
			{
				infoCanvasPopup.SetActive(false);
			}

			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Lee Walker", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				victimNameText.text = victim.name;
				locationText.text = victim.location;
				dateText.text = victim.date;
				descriptionText.text = victim.description;

				infoButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage(victim.linkToSite); });
				//Uncomment me when victim data is linked to specfic point cloud expereinces
				//ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/Sites/LeeWalker"); });
				ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/AR"); });


				//Show the "not near marker" popup
				//ARButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(cycleMarkerOutOfRange()); });

				//This function will be replaced this Jonathans Code to take them to directions
				directionsButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage("https://www.google.com/maps/dir//35.158065,%20-90.049201"); });
				//Set the global variable for which victim wer're on. We'll use this to decide with 3D scene to display
				PlayerPrefs.SetString("CurrentVictim", "Lee Walker");
			}
			else { Debug.Log("Victim not found in the dictionary"); }

		}

		//------------------------------------------------------------------------------------------

		//-------------------------------
		//Check the tag of the gameobject clicked and see if that victim exists in our dictionary
		if (gameObject.CompareTag("Unnamed Victim"))
		{
			Debug.Log("We clicked Unnamed Victim");

			//Turn on the prefab InfoCanvasPopup to show information about the site
			if (!infoCanvasPopup.activeInHierarchy)
			{
				infoCanvasPopup.SetActive(true);
			}
			//If the popup is already showing, turn it off
			else
			{
				infoCanvasPopup.SetActive(false);
			}

			//Check our dictionary and see if the victim info is currently in it
			if (localVictimData.TryGetValue("Unnamed Victim", out LynchingVictim victim))
			{
				Debug.Log("Victim  found in the dictionary");
				victimNameText.text = victim.name;
				locationText.text = victim.location;
				dateText.text = victim.date;
				descriptionText.text = victim.description;
				//Uncomment me when victim data is linked to specfic point cloud expereinces
				//ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/Sites/Unnamed"); });

				//Show the "not near marker" popup
				//ARButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(cycleMarkerOutOfRange()); });
				ARButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Scenes/AR"); });

				infoButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage(victim.linkToSite); });

				//This function will be replaced this Jonathans Code to take them to directions
				directionsButton.GetComponent<Button>().onClick.AddListener(delegate { openWebPage("https://www.google.com/maps/dir//35.152427,%20-90.048603"); });
				//Set the global variable for which victim wer're on. We'll use this to decide with 3D scene to display
				PlayerPrefs.SetString("CurrentVictim", "Unnamed Victim");
			}
			else { Debug.Log("Victim not found in the dictionary"); }

		}

		//------------------------------------------------------------------------------------------
	}

	public void openWebPage(string victimInfoURL)
	{
		Application.OpenURL(victimInfoURL);
		BrowserOpener browser = this.gameObject.GetComponent<BrowserOpener>();
		browser.openBrowserURL(victimInfoURL);
	}

	public IEnumerator cycleMarkerOutOfRange()
	{
		//Turn the popup on
		OutOfRangeMarker.SetActive(true);
		//Wait 3 seconds
		yield return new WaitForSeconds(3);
		//Turn the popup off
		OutOfRangeMarker.SetActive(false);


	}
}
