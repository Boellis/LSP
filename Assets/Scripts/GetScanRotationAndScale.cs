using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Lean.Touch;
using System;

public class GetScanRotationAndScale : MonoBehaviour
{
    public TMP_Text scaleX;
    public TMP_Text scaleY;
    public TMP_Text scaleZ;

    public TMP_Text rotationX;
    public TMP_Text rotationY;
    public TMP_Text rotationZ;

    public TMP_Text positionX;
    public TMP_Text positionY;
    public TMP_Text positionZ;

    public Slider rotXSlider;
    public Slider rotYSlider;
    public Slider rotZSlider;

    public Slider posXSlider;
    public Slider posYSlider;
    public Slider posZSlider;

    public GameObject targetObject;
    public LeanDragTranslate realtimePosition;
    public LeanPinchScale realtimeScale;

    public GameObject parentObject;

    public GameObject XRCameraObj;
    public TMP_Text camPositionX;
    public TMP_Text camPositionY;
    public TMP_Text camPositionZ;
    public TMP_Text camRotationX;
    public TMP_Text camRotationY;
    public TMP_Text camRotationZ;

    public Button plusX, minusX, plusY, minusY, plusZ, minusZ;

    public Button plusRotX, minusRotX, plusRotZ, minusRotZ;



    public float step = 1f;
    public float rotationStep = 1f;


    // Start is called before the first frame update
    void Start()
    {

        rotXSlider.value = 0f;//normalizeScanRotation(targetObject.transform.localEulerAngles)["x"];
        rotYSlider.value = 0f;//normalizeScanRotation(targetObject.transform.localEulerAngles)["y"];
        rotZSlider.value = 0f;//normalizeScanRotation(targetObject.transform.localEulerAngles)["z"];

        rotXSlider.onValueChanged.AddListener(UpdateXRotation);
        rotYSlider.onValueChanged.AddListener(UpdateYRotation);
        rotZSlider.onValueChanged.AddListener(UpdateZRotation);

        posXSlider.onValueChanged.AddListener(UpdateXPosition);
        posYSlider.onValueChanged.AddListener(UpdateYPosition);
        posZSlider.onValueChanged.AddListener(UpdateZPosition);

        // Subscribe to the button's onClick events - Parent Position
        plusX.onClick.AddListener(IncreaseX);
        minusX.onClick.AddListener(DecreaseX);
        plusY.onClick.AddListener(IncreaseY);
        minusY.onClick.AddListener(DecreaseY);
        plusZ.onClick.AddListener(IncreaseZ);
        minusZ.onClick.AddListener(DecreaseZ);

        // Subscribe to the button's onClick events - Reference Cube rotation
        plusRotX.onClick.AddListener(IncreaseXRotation);
        minusRotX.onClick.AddListener(DecreaseXRotation);
        plusRotZ.onClick.AddListener(IncreaseZRotation);
        minusRotZ.onClick.AddListener(DecreaseZRotation);
    }


    // Update is called once per frame
    void Update()
    {
        //Position of the topmost parent scan object i.e the object that leandragtranslate is attached to. 
        /*positionX.text = realtimePosition.getScanPosition().position.x.ToString();
        positionY.text = realtimePosition.getScanPosition().position.y.ToString();
        positionZ.text = realtimePosition.getScanPosition().position.z.ToString();*/

        positionX.text = parentObject.transform.position.x.ToString();
        positionY.text = parentObject.transform.position.y.ToString();
        positionZ.text = parentObject.transform.position.z.ToString();


        //Get the rotation of the reference cube that parents the scan model
        rotationX.text = normalizeScanRotation(targetObject.transform.localEulerAngles)["x"].ToString();
        rotationY.text = normalizeScanRotation(targetObject.transform.localEulerAngles)["y"].ToString();
        rotationZ.text = normalizeScanRotation(targetObject.transform.localEulerAngles)["z"].ToString();

        //Get the scale of the topmost parent scan gameobject
        scaleX.text = realtimeScale.getScanScale().x.ToString();
        scaleY.text = realtimeScale.getScanScale().y.ToString();
        scaleZ.text = realtimeScale.getScanScale().z.ToString();

        //Position of the XR Camera 
        camPositionX.text = XRCameraObj.transform.position.x.ToString();
        camPositionY.text = XRCameraObj.transform.position.y.ToString();
        camPositionZ.text = XRCameraObj.transform.position.z.ToString();


        //Rotation of the XR Camera 
        camRotationX.text = normalizeScanRotation(XRCameraObj.transform.localEulerAngles)["x"].ToString();
        camRotationY.text = normalizeScanRotation(XRCameraObj.transform.localEulerAngles)["y"].ToString();
        camRotationZ.text = normalizeScanRotation(XRCameraObj.transform.localEulerAngles)["z"].ToString();

    }

    public void UpdateXRotation(float value)
    {
        Vector3 currentRotation = targetObject.transform.localEulerAngles;
        currentRotation.x = value;
        targetObject.transform.localEulerAngles = currentRotation;
        //rotationX.text = currentRotation.x.ToString();
    }

    public void UpdateYRotation(float value)
    {
        Vector3 currentRotation = targetObject.transform.localEulerAngles;
        currentRotation.y = value;
        targetObject.transform.localEulerAngles = currentRotation;
        //rotationY.text = currentRotation.y.ToString();

    }

    public void UpdateZRotation(float value)
    {
        Vector3 currentRotation = targetObject.transform.localEulerAngles;
        currentRotation.z = value;

        targetObject.transform.localEulerAngles = currentRotation;
        //rotationZ.text = currentRotation.z.ToString();
    }

    //Update the position of the top most object
    public void UpdateXPosition(float value)
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.x = value;
        parentObject.transform.position = currentPosition;
    }

    public void UpdateYPosition(float value)
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.y = value;
        parentObject.transform.position = currentPosition;
    }

    public void UpdateZPosition(float value)
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.z = value;
        parentObject.transform.position = currentPosition;
    }

    public Dictionary<string, float> normalizeScanRotation(Vector3 scanTransform)
    {
        Dictionary<string, float> normalizedAngles = new Dictionary<string, float>();

        float Xangle = (scanTransform.x > 180) ?
                       scanTransform.x - 360 :
                       scanTransform.x;
        normalizedAngles.Add("x", Xangle);
        float yAngle = (scanTransform.y > 180) ?
                      scanTransform.y - 360 :
                      scanTransform.y;
        normalizedAngles.Add("y", yAngle);

        float zAngle = (scanTransform.z > 180) ?
                       scanTransform.z - 360 :
                       scanTransform.z;
        normalizedAngles.Add("z", zAngle);

        return normalizedAngles;

    }
    //Start parent position
    public void IncreaseX()
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.x += step;
        parentObject.transform.position = currentPosition;
    }

    public void DecreaseX()
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.x -= step;
        parentObject.transform.position = currentPosition;
    }

    public void IncreaseY()
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.y += step;
        parentObject.transform.position = currentPosition;
    }

    public void DecreaseY()
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.y -= step;
        parentObject.transform.position = currentPosition;
    }

    public void IncreaseZ()
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.z += step;
        parentObject.transform.position = currentPosition;
    }

    public void DecreaseZ()
    {
        Vector3 currentPosition = parentObject.transform.position;
        currentPosition.z -= step;
        parentObject.transform.position = currentPosition;
    }
    //End parent position

    //Start reference cube rotation
    private void IncreaseXRotation()
    {
        Vector3 currentRotation = targetObject.transform.eulerAngles;
        currentRotation.x += rotationStep;
        targetObject.transform.eulerAngles = currentRotation;
    }

    private void DecreaseXRotation()
    {
        Vector3 currentRotation = targetObject.transform.eulerAngles;
        currentRotation.x -= rotationStep;
        targetObject.transform.eulerAngles = currentRotation;
    }

    private void IncreaseZRotation()
    {
        Vector3 currentRotation = targetObject.transform.eulerAngles;
        currentRotation.z += rotationStep;
        targetObject.transform.eulerAngles = currentRotation;
    }

    private void DecreaseZRotation()
    {
        Vector3 currentRotation = targetObject.transform.eulerAngles;
        currentRotation.z -= rotationStep;
        targetObject.transform.eulerAngles = currentRotation;
    }
    //End reference cube rotation

}
