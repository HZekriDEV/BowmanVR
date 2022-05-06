using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HUD : MonoBehaviour
{
    public XRController controller = null;
    private Vector3 value;

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.GetComponent<Transform>().position;
    }
}
