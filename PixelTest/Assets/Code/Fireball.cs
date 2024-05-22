using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 25;
    public float speed = 1f;
    public Rigidbody2D rb;

    void Start()
    {
        // Ustawienie prêdkoœci pocz¹tkowej w momencie stworzenia fireball
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            if(collision.gameObject.GetComponent<HealthManager>()!=null)
        collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);  

        Destroy(this.gameObject);



    }
        
        
    }

