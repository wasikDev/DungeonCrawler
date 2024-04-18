using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AiController : MonoBehaviour
{
    public GameObject target;
    public float speed = 1f;
    public float range = 10;
    float distance;

    public int damage = 25; // Example damage value
    public float cooldown;
    private float nextAttack;

    bool facingRight = true;

    public int maxHealth = 100;
    public int currentHealth;

    //public HealthBar healthBar;

    //public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {



       


        distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (distance < range)
        {
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip(); // Funkcja obracaj¹ca AI
            }
            transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);


        }







    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
       // healthBar.setHealth(currentHealth);
       if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time > nextAttack) 
        {
            
            var player = collision.gameObject.GetComponent<CharacterController2D>();
            player.TakeDamage(damage);
            nextAttack = Time.time + cooldown;
        }
    }


}

    

