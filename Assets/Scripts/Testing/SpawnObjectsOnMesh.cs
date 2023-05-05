using UnityEngine;

public class SpawnObjectsOnPointCloud : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public int numberOfObjects;
    public float yOffset;
    //public float minObjectScale = 1.0f; // The minimum scale of the spawned objects (uniform scale)
    //public float maxObjectScale = 1.0f; // The maximum scale of the spawned objects (uniform scale)
    public float maxZ;
    public float minZ;

    private Vector3[] points;

    void Start()
    {
        points = GetComponent<MeshFilter>().mesh.vertices;

        // Spawn objects on the point cloud
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        int randomIndex = Random.Range(0, points.Length);
        Vector3 randomPoint = points[randomIndex];

        if (randomPoint.z > maxZ || randomPoint.z < minZ)
        {
            return;
        }

        Vector3 worldPoint = transform.TransformPoint(randomPoint) + Vector3.up * yOffset;
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        GameObject instance = Instantiate(objectToSpawn, worldPoint, Quaternion.Euler(0, Random.Range(0, 360), 0));

        // Set a random uniform scale for the spawned object between minObjectScale and maxObjectScale
        //float randomUniformScale = Random.Range(minObjectScale, maxObjectScale);
        //instance.transform.localScale = new Vector3(randomUniformScale, randomUniformScale, randomUniformScale);

        instance.transform.SetParent(transform);
    }
}
