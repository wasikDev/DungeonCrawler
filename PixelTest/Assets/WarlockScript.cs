using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarlockScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint; // Punkt, z którego bêd¹ wystrzeliwane pociski
    public Transform player;
    public float shootingRange = 10.0f;
    public float shootInterval = 1.5f;
    public float moveSpeed = 5.0f;
    public float minDistanceFromPlayer = 5.0f;
    private Rigidbody2D rb;
    public Animator animator;
    private float shootTimer;
    public HealthManager healthManager;
    public float range;
   


    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 directionToPlayer = (Vector2)(player.position - transform.position);
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector2 moveDirection = Vector2.zero;
        // Rotate towards the player - horizontal axis only
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        // Movement logic
        if (distanceToPlayer < range)
        {
            if (distanceToPlayer > shootingRange)
            {
                // Move closer if too far
                moveDirection = directionToPlayer.normalized * moveSpeed;
            }
            else if (distanceToPlayer < minDistanceFromPlayer)
            {
                // Move away if too close
                moveDirection = -directionToPlayer.normalized * moveSpeed;
            }
        }

        rb.velocity = moveDirection;

        // Shooting logic
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval && distanceToPlayer <= shootingRange)
        {
            
            shootTimer = 0;
            animator.SetBool("Attack", true);
            StartCoroutine(ShootWithDelay(directionToPlayer.normalized));
        }
       

        animator.SetFloat("Speed", rb.velocity.magnitude);
        
    }

    IEnumerator ShootWithDelay(Vector2 direction)
    {
        yield return new WaitForSeconds(0.7f); // Wait for 0.5 seconds to sync with animation
        Shoot(direction); // Fire the projectile
        animator.SetBool("Attack", false); // Optionally turn off attack animation
    }

   

    void Shoot(Vector2 direction)
    {
        
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 20f; // Adjust speed as necessary
    }



}
