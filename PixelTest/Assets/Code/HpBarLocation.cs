using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarLocation : MonoBehaviour
{
    public Transform parent;
    public float offsetX = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + offsetX, parent.transform.position.z);
    }
}
