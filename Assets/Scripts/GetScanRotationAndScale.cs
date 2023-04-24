using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        positionX.text = this.gameObject.transform.position.x.ToString();
        positionY.text = this.gameObject.transform.position.y.ToString();
        positionZ.text = this.gameObject.transform.position.z.ToString();

        rotationX.text = this.gameObject.transform.rotation.x.ToString();
        rotationY.text = this.gameObject.transform.rotation.y.ToString();
        rotationZ.text = this.gameObject.transform.rotation.z.ToString();

        scaleX.text = this.gameObject.transform.localScale.x.ToString();
        scaleY.text = this.gameObject.transform.localScale.y.ToString();
        scaleZ.text = this.gameObject.transform.localScale.z.ToString();

    }
}
