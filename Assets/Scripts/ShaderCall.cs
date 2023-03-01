using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ShaderCall : MonoBehaviour
{
     
    protected ComputeBuffer bufferPoints;
    protected ComputeBuffer bufferColors;
    CommandBuffer commandBuffer;
    CommandBuffer commandBufferDepth;
    public bool forceDepthBufferPass;  
    public bool useCommandBuffer; 
    public Material cloudMaterial;
    public int numberOfPointsPerFrame = 2500000;  
    //public PointCloudRenderer rend;

    //public IPCapParser Parser;
  
    public CameraEvent camDrawPass = CameraEvent.AfterForwardOpaque;
    public CameraEvent camDepthPass = CameraEvent.AfterDepthTexture;
    private MeshFilter mf;
    public Mesh mesh;
    //public int GhostFrames;  
    
    void Start ()
    {
        Debug.Log($"started at {DateTime.Now}");
       //Parser = GetComponent<IPCapParser>(); 
        if (useCommandBuffer == true)
        {
            commandBuffer = new CommandBuffer();
            Camera.main.AddCommandBuffer(camDrawPass, commandBuffer);
            
        }

        if (forceDepthBufferPass == true)
        {
            //depthMaterial = cloudMaterial;
            commandBufferDepth = new CommandBuffer();
            Camera.main.AddCommandBuffer(camDepthPass, commandBufferDepth);
            
        }

        mesh = new Mesh();
        mf = GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;
        
        mesh.MarkDynamic();

        mf.mesh = mesh;
        mesh.indexFormat = IndexFormat.UInt32;
        //mesh = new Mesh();
        //mf.sharedMesh = mesh;     
        InitBuffersForStreaming();
        
    }



   void InitBuffersForStreaming()
        {
            bufferPoints?.Dispose();
            bufferPoints = new ComputeBuffer(numberOfPointsPerFrame, 12); 
            bufferColors?.Dispose();
            bufferColors = new ComputeBuffer(numberOfPointsPerFrame, 4);
            
            if (useCommandBuffer == true)
            {
                commandBuffer.DrawProcedural(Matrix4x4.identity, cloudMaterial, 0, MeshTopology.Points, numberOfPointsPerFrame, 1);
            }

            if (forceDepthBufferPass == true)
            {
                commandBufferDepth.DrawProcedural(Matrix4x4.identity, cloudMaterial, 0, MeshTopology.Points,
                    numberOfPointsPerFrame, 1);
            }
        }

   public void setParticles(Vector3[] verts, Color32[] cols)
   {
       try
       {
          // var mr = gameObject.GetComponent<MeshRenderer>();
      /* if (mr == null)
       {
           mr = gameObject.AddComponent<MeshRenderer>();
       }

       var mf = gameObject.GetComponent<MeshFilter>();
       if (mf == null)
       {
           mf = gameObject.AddComponent<MeshFilter>();
       }*/


       /*  if (verts.Length !=   numberOfPointsPerFrame)
         {
             InitBuffersForStreaming();
           
         }*/
       if (verts == null) return;
       numberOfPointsPerFrame = (int) (verts.Length);

       var tris = new int[numberOfPointsPerFrame];

       // Debug.Log(cols[0].ToString());


 
       
       
       var vers = new Vector3[numberOfPointsPerFrame];
       var cls = new Color32[numberOfPointsPerFrame];

       for (var i = 0; i < numberOfPointsPerFrame; i++)
       {
           vers[i] = verts[i];
           cls[i] = cols[i];
           tris[i] = i;
       }
        
       mesh.vertices = vers; //Enumerable.Range(0,numberOfPointsPerFrame).Select(x=> verts[x]).ToArray();
       mesh.colors32 = cls; //Enumerable.Range(0,numberOfPointsPerFrame).Select(x=> cols[x]).ToArray();

       mesh.SetIndices(tris, MeshTopology.Points, 0);

       
       var daatta = verts;
       bufferPoints.SetData(daatta);
       cloudMaterial.SetBuffer("buf_Points", bufferPoints);
       bufferColors.SetData(cols);
       cloudMaterial.SetBuffer("buf_Colors", bufferColors);
       }
       catch (Exception e)
       {
           Console.WriteLine(e);
           Debug.Log($"failed at {DateTime.Now}");
       }

   }
        
     
}

