using Cinemachine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterController2D_Wizard : MonoBehaviour
{
    public GameObject player;
    public GameObject rotateObject;
    public CinemachineVirtualCamera virtualCamera;
    public VisualEffect vfxRenderer;

    public float runSpeed = 1f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool facingRight = true;

    public Animator animator;
   
    public Transform firepoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public GameObject MisslePrefab;

    public float cooldown;
    float nextShot;

    public float attackCooldown;
    float nextAttack;

   

    public int damage = 20;

    private void Start()
    {
        virtualCamera.Follow= player.transform;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

        transform.position += Vector3.right * horizontalMove * Time.deltaTime;
        transform.position += Vector3.up * verticalMove * Time.deltaTime;



        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextAttack)
        {
            StartCoroutine(Attack());
            nextAttack = Time.time + attackCooldown;
        }
        else 
            animator.SetBool("attack", false);



        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextShot)
        {
            Shoot();
            nextShot = Time.time + cooldown;
        }

        if (horizontalMove < 0 && facingRight || horizontalMove > 0 && !facingRight)
            Flip();

        vfxRenderer.SetVector3("ColliderPos", gameObject.transform.localPosition);
        animator.SetFloat("speed", Math.Abs(horizontalMove) + Math.Abs(verticalMove));


       


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
    
    
    
    


    IEnumerator Attack()
    {
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(0.35f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firepoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies) {

           enemy.GetComponent<HealthManager>().TakeDamage(damage);

            
            Debug.Log("We hit " + enemy.name);
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(firepoint.position, attackRange);
    }

}