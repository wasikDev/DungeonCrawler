using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class CharacterController2D : MonoBehaviour
{
    //public VisualEffect vfxRenderer;
    Rigidbody2D rb;

   
    
    public float aniSpeed = 1.0f;
    public float runSpeed = 1f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool facingRight = true;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public Animator animator;
    
    void Start()
    {
        rb =gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }


    void Update()
    {
       
        

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        

        

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("attack", true);

        }
        else
        {
            animator.SetBool("attack", false);
        }
        

            if (horizontalMove <0 && facingRight)
            {
            Flip();
            }

             else if (horizontalMove > 0 && !facingRight)
            {
            Flip();
             }




        animator.SetFloat("speed", Math.Abs(horizontalMove) + Math.Abs(verticalMove));
        animator.speed = aniSpeed;


       // vfxRenderer.SetVector3("ColliderPos", gameObject.transform.localPosition);
           

            
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
   


    
        
    


}