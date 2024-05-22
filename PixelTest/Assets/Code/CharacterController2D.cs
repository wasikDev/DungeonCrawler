using Cinemachine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterController2D : MonoBehaviour
{
    public GameObject attackIcnon;
    public GameObject skillIcnon;

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

   

    public float attackCooldown;
    float nextAttack;

    public GameObject flamethrower;
    public float flamethrowerDuration = 1f; // Czas trwania miotacza ognia
    private float flamethrowerCooldown = 5.0f; // Czas odnowienia miotacza ognia
    private float nextFlamethrowerTime = 0; // Nastêpny dostêpny czas ataku

    public int damage = 20;

    private void Start()
    {
        virtualCamera.Follow = player.transform;
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

        if(Time.time > nextAttack)
        {
            attackIcnon.SetActive(true);
        }
        else attackIcnon.SetActive(false);


        if (Time.time > nextFlamethrowerTime)
        {
            skillIcnon.SetActive(true);
        }
        else skillIcnon.SetActive(false);

        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextFlamethrowerTime)
        {
            StartCoroutine(ActivateFlamethrower());
            nextFlamethrowerTime = Time.time + flamethrowerCooldown;
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

    IEnumerator ActivateFlamethrower()
    {
        flamethrower.SetActive(true); // W³¹cz trigger i efekty
        animator.SetBool("skill", true); // Aktywuj animacjê miotacza ognia

        yield return new WaitForSeconds(flamethrowerDuration);

        flamethrower.SetActive(false); // Wy³¹cz trigger i efekty
        animator.SetBool("skill", false); // Dezaktywuj animacjê miotacza ognia
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
        yield return new WaitForSeconds(0.45f);
        Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(firepoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy2 in hitEnemies2)
        {

            enemy2.GetComponent<HealthManager>().TakeDamage(damage);


            Debug.Log("We hit " + enemy2.name);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(firepoint.position, attackRange);
    }

}