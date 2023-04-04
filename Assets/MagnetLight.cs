using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetLight : MonoBehaviour
{
    public Material hollowboxMaterial;
    public Material highligtedboxMaterial;
    public Renderer magnetRenderer;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ComponentElement"){
            magnetRenderer.sharedMaterial = highligtedboxMaterial;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "ComponentElement"){
            magnetRenderer.sharedMaterial = hollowboxMaterial;
        }
    }
}
