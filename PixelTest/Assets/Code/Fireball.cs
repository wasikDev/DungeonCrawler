using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 25;
    public float speed= 1f;
    public Rigidbody2D rb;
    
    void Update()
    {
        rb.velocity = transform.right * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" ) 
        {
            var target = collision.gameObject.GetComponent<HealthManager>();
            target.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Enemy") 
        {
            var target = collision.gameObject.GetComponent<AiController>();
            target.TakeDamage(damage);
        }

        Debug.Log("Destroy!");
        Destroy(this.gameObject);
        
    }

}
