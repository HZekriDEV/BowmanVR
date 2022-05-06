using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateHand : MonoBehaviour
{
    public XRController controller = null;

    Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float gripButton = 0;
        animator.SetFloat("Press", gripButton);
        controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out gripButton);
        animator.SetFloat("Press", gripButton);
        
    }
}
