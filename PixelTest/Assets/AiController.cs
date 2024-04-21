using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AiController : MonoBehaviour
{
    public HealthBar hpBar;
    public GameObject target;
   // public GameObject RotationTarget;
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

    private Vector2 startpos;

    void Start()
    {
        currentHealth = maxHealth;
        startpos = transform.position;

    }


    void Update()
    {

        distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (distance < range && Time.time > nextAttack)
        {
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip(); // Funkcja obracaj¹ca AI
            }
            transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);


        }else
        {
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip(); // Funkcja obracaj¹ca AI
            }
            transform.position = Vector3.MoveTowards(this.transform.position, startpos, speed * Time.deltaTime);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpBar.setHealth(currentHealth);
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

    

