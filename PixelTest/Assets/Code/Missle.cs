using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Transform firepoint;
    public GameObject MisslePrefab;
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }

        
    }

    void Shoot () { 
    Instantiate(MisslePrefab, firepoint.position, firepoint.rotation);
    }
}
