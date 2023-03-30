using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToPoints : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var smr=GetComponent<SkinnedMeshRenderer>();
        var mesh = smr.sharedMesh;
        var indices=mesh.GetIndices(0);
        mesh.SetIndices(indices,MeshTopology.Points,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
