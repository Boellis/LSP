using UnityEngine;
using System.Collections;

public class BrowserOpener : MonoBehaviour {

	public void openBrowserURL(string pageToOpen) {
		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.displayURLAsPageTitle = false;
		options.pageTitle = "The Lynching Sites Project";

		/*options.barBackgroundColor = "#FF0000";
		options.textColor = "#FF0000";
		options.browserBackgroundColor = "#FF0000";
		*/

		InAppBrowser.OpenURL(pageToOpen, options);
	}

	public void Start()
	{
	}
	public void OnClearCacheClicked() {
		Debug.Log("Clear Cache!");
		InAppBrowser.ClearCache();
	}
}
