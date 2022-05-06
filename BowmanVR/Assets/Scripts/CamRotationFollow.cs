using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotationFollow : MonoBehaviour
{
    public Transform mainCamera = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.rotation;
    }
}
