using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Material dissolveMaterial;

    void Update()
    {
        // Adjust the dissolve progress over time (0 to 1)
        float dissolveProgress = Mathf.PingPong(Time.time, 1.0f);
        dissolveMaterial.SetFloat("_AlphaTransitionProgress", dissolveProgress);
    }
}