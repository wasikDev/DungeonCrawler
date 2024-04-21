using System;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject rotateObject;
    public VisualEffect vfxRenderer;
    public float runSpeed = 1f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool facingRight = true;
    public Animator animator;
    public HealthManager healthManager; // Reference to HealthManager

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

        transform.position += Vector3.right * horizontalMove * Time.deltaTime;
        transform.position += Vector3.up * verticalMove * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            animator.SetBool("attack", true);
        else
            animator.SetBool("attack", false);

        if (horizontalMove < 0 && facingRight || horizontalMove > 0 && !facingRight)
            Flip();

        vfxRenderer.SetVector3("ColliderPos", gameObject.transform.localPosition);
        animator.SetFloat("speed", Math.Abs(horizontalMove) + Math.Abs(verticalMove));
    }

<<<<<<< Updated upstream
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


=======
>>>>>>> Stashed changes
    void Flip()
    {
        facingRight = !facingRight;
        rotateObject.transform.Rotate(0f, 180f, 0f);
    }
}
