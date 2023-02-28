using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAboutPageOnStart : MonoBehaviour
{
    public BrowserOpener inAppBrowser;
    // Start is called before the first frame update
    void Start()
    {
        inAppBrowser.openBrowserURL("https://lynchingsitesmem.org/about");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
