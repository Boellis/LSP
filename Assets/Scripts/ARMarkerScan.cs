using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARMarkerScan : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    //[SerializeField] private GameObject[] arObjectsToPlace;
    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject prefabToSpawn;

    private void Awake()
    {
        /*foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            newARObject.SetActive(false);
            arObjects.Add(arObject.name, newARObject);
        }*/
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            //UpdateARObject(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //UpdateARObject(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            SpawnPrefabOnImage(trackedImage);
            //arObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateARObject(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject arObject = arObjects[imageName];

        arObject.transform.position = trackedImage.transform.position;
        arObject.transform.rotation = trackedImage.transform.rotation;
        arObject.SetActive(true);
    }

    private void SpawnPrefabOnImage(ARTrackedImage trackedImage)
    {
        GameObject newObject = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
        newObject.transform.SetParent(trackedImage.transform);
    }
}