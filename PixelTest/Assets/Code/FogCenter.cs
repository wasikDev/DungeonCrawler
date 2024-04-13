using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FogCenter : MonoBehaviour
{

    public VisualEffect vfxRenderer;
    void Update()
    {
        vfxRenderer.SetVector3("ColliderPos", transform.position);
    }
}
