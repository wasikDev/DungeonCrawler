using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Transform firepoint;
    public GameObject MisslePrefab;
   
    public float cooldown;
    float nextShot;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextShot)
        {
            Shoot();
            nextShot = Time.time+cooldown;
        }

        
    }

    void Shoot () { 
    Instantiate(MisslePrefab, firepoint.position, firepoint.rotation);
    }
}
