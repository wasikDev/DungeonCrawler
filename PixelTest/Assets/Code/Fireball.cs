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
        if (collision.gameObject.tag == "Player")
        {
            var target = collision.gameObject.GetComponent<HealthManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            var target = collision.gameObject.GetComponent<HealthManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            

        }

        else if (collision.gameObject.tag == "EnemyRanged")
        {
            var target = collision.gameObject.GetComponent<HealthManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }


        }
        
        Destroy(this.gameObject);
    }
}
