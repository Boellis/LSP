using UnityEngine;
using System.Collections;

public class BrowserOpener : MonoBehaviour {

	//public string pageToOpen = "https://www.google.com";

	// check readme file to find out how to change title, colors etc.
	public void openBrowserURL(string pageToOpen) {
		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.displayURLAsPageTitle = false;
		options.pageTitle = "LSP Browser";

		InAppBrowser.OpenURL(pageToOpen, options);
	}

	public void OnClearCacheClicked() {
		Debug.Log("Clear Cache!");
		InAppBrowser.ClearCache();
	}

	public void openVictimProfileURL()
	{
		string currentVictim = PlayerPrefs.GetString("CurrentVictim");
		if (currentVictim == "Ell Persons")
		{
			openBrowserURL("https://lynchingsitesmem.org/lynching/ell-persons");
		}
		else if (currentVictim == "Lee Walker")
		{
			openBrowserURL("https://lynchingsitesmem.org/lynching/lee-walker");
		}
		else if (currentVictim == "Jesse Lee Bond")
		{
			openBrowserURL("https://lynchingsitesmem.org/lynching/jesse-lee-bond");
		}
		else if (currentVictim == "Unnamed Victim")
		{
			openBrowserURL("https://lynchingsitesmem.org/lynching/name-unknown-downtown-memphis-1851");
		}
		else if (currentVictim == "People's Grocery")
		{
			openBrowserURL("https://lynchingsitesmem.org/lynching/peoples-grocery-lynchings-thomas-moss-will-stewart-calvin-mcdowell");
		}
		else
		{
			openBrowserURL("https://lynchingsitesmem.org/");
		}
	}
}
