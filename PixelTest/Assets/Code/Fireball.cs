using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed= 1f;
    public Rigidbody2D rb;
    
    void Update()
    {
        rb.velocity = transform.right * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroy!");
        Destroy(this.gameObject);
        
    }

}
