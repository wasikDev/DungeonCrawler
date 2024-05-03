using System;
using Unity.VisualScripting;
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
    public Transform firepoint;
    public GameObject MisslePrefab;

    public float cooldown;
    float nextShot;
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


        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextShot)
        {
            Shoot();
            nextShot = Time.time + cooldown;
        }


        UpdateFirePointPosition(horizontalMove, verticalMove);
    }

    void Flip()
    {
        facingRight = !facingRight;
        rotateObject.transform.Rotate(0f, 180f, 0f);
    }

    void Shoot()
    {
        
        Vector2 shootingDirection = new Vector2(horizontalMove, verticalMove).normalized;
        if (shootingDirection == Vector2.zero)
            shootingDirection = facingRight ? Vector2.right : Vector2.left;

        GameObject projectile = Instantiate(MisslePrefab, firepoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = shootingDirection * 20f; // Adjust the speed as necessary

        // Rotate the projectile to align with the direction
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void UpdateFirePointPosition(float horizontal, float vertical)
    {
        float firePointOffset = 0.5f; // Sta³a odleg³oœæ firepoint od gracza

        Vector3 direction = new Vector3(horizontal, vertical, 0);
        if (direction == Vector3.zero)
        {
            // When there is no movement, place the firepoint horizontally in front of the player
            firepoint.transform.position = transform.position + Vector3.right * (facingRight ? firePointOffset : -firePointOffset);
        }
        else
        {
            // Apply normalization only if there is movement
            direction.Normalize();
            firepoint.transform.position = transform.position + direction * firePointOffset;
        }
    }

}