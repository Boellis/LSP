using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LynchingVictim
{
    public string name;
    public string location;
    public Image image;
    public string date;
    public string linkToSite;
    public bool markerOnSite;

    public LynchingVictim(string _name, string _location, Image _image, string _date, string _linkToSite, bool _markerOnSite)
	{
        name = _name;
        location = _location;
        image = _image;
        date = _date;
        linkToSite = _linkToSite;
        markerOnSite = _markerOnSite;
	}

    public LynchingVictim(string _name, string _location, string _date, string _linkToSite, bool _markerOnSite)
    {
        name = _name;
        location = _location;
        date = _date;
        linkToSite = _linkToSite;
        markerOnSite = _markerOnSite;
    }
}
