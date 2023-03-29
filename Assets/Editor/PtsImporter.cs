using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;
[ScriptedImporter(1, "pts")]
public class PtsImporter : ScriptedImporter
{
    private int limitPoints = 65000;
    public float scale = 1;
    public bool invertYZ;
    public Material matVertex;// = new Material(Shader.Find("Custom/VertexColor"));
    /*
------------------
NOTE TO FUTURE JONATHAN AND BRANDON
The Point Cloud/Disc shader probably only works on PC
The Point Cloud/Point shader does not work on DX11, but does work in OpenGL & Metal
------------------
    */
    public override void OnImportAsset(AssetImportContext ctx)
    {
        bool saveMat = false;
        if (matVertex == null)
        {
            matVertex = new Material(Shader.Find("Point Cloud/Point"));
            saveMat = true;
        }
        var filename = Path.GetFileName(ctx.assetPath);
        StreamReader sr = new StreamReader(ctx.assetPath);
        string[] buffer;
        string line;
        var points = new List<Vector3>();
        var normals = new List<Vector3>();
        var colors = new List<Color>();
        var indices = new List<int>();
        int index = 0;
        int meshCount = 0;
        var pointCloud = new GameObject(filename);
        Vector3 worldOffset = Vector3.zero;

        void MakeMesh()
        {
            var tempMesh = new Mesh();
            //first we create a point mesh like normal.
            tempMesh.name = filename + "_mesh_" + meshCount;
            tempMesh.SetVertices(points);
            tempMesh.SetNormals(normals);
            tempMesh.SetColors(colors);
            tempMesh.SetIndices(indices, MeshTopology.Points, 0);
            //then we get the center as an offset, and shift all the points accordingly
            Vector3 offset = tempMesh.bounds.center;
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = points[i] - offset;
            }
            tempMesh.SetVertices(points);
            tempMesh.RecalculateBounds();
            ctx.AddObjectToAsset("mesh_" + meshCount, tempMesh);

            var pointGroup = new GameObject(filename + "_" + meshCount);
            var mf = pointGroup.AddComponent<MeshFilter>();
            var mr = pointGroup.AddComponent<MeshRenderer>();
            mf.mesh = tempMesh;
            mr.material = matVertex;
            //we check if this is our first mesh to be at (0,0,0), if so we save the offset as a global offset
            //otherwise we set the transform as our offset and subtract our global offset
            if (worldOffset == Vector3.zero)
                worldOffset = offset;
            else
                pointGroup.transform.position = offset - worldOffset;
            pointGroup.transform.parent = pointCloud.transform;
        }
        //while parsing through the file, if we hit the vertex limit, create a mesh and game object, and add it to the context.
        //reset everything and continue.
        bool isInt = true;
        while (!sr.EndOfStream)
        {
            buffer = sr.ReadLine().Split();
            if (invertYZ)
            {
                points.Add(new Vector3(float.Parse(buffer[0]) * scale, float.Parse(buffer[2]) * scale, float.Parse(buffer[1]) * scale));
                normals.Add(new Vector3(float.Parse(buffer[0]) * scale, float.Parse(buffer[2]) * scale, float.Parse(buffer[1]) * scale));
            }
            else
            {
                points.Add(new Vector3(float.Parse(buffer[0]) * scale, float.Parse(buffer[1]) * scale, float.Parse(buffer[2]) * scale));
                normals.Add(new Vector3(float.Parse(buffer[0]) * scale, float.Parse(buffer[1]) * scale, float.Parse(buffer[2]) * scale));
            }
            int r;
            if (isInt && int.TryParse(buffer[3], out r))
            {
                colors.Add(new Color(r / 255.0f, int.Parse(buffer[4]) / 255.0f, int.Parse(buffer[5]) / 255.0f));
            }
            else
            {
                isInt = false;
                colors.Add(new Color(float.Parse(buffer[3]), float.Parse(buffer[4]), float.Parse(buffer[5])));
            }

            indices.Add(index);
            index++;
            if (index == limitPoints)
            {
                //make a mesh
                MakeMesh();

                points.Clear();
                normals.Clear();
                colors.Clear();
                indices.Clear();
                index = 0;

                meshCount++;
                //reset the indices and index counter
            }
        }
        //if index is greater than 0, that means we have points that weren't added to a mesh.
        if (index > 0)
        {
            MakeMesh();
        }
        if (saveMat)
        {
            ctx.AddObjectToAsset(filename + "_mat", matVertex);
        }
        ctx.AddObjectToAsset(filename, pointCloud);
        ctx.SetMainObject(pointCloud);

    }

}