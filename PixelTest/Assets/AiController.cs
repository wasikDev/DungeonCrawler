using System.Collections;
using UnityEngine;

public class AiController : MonoBehaviour
{
    GameObject target;
    public float speed = 1f;
    public float range = 10f;
    float distance;
    
    public int damage = 25; // Example damage value
    public float cooldown = 2f;
    private float nextAttack;
    public float attackDistance = 1.5f;
    bool facingRight = true;
    private Rigidbody2D rb;

    public Transform firepoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Vector2 startpos;
    private Vector2 previousPosition;

    private bool isAttacking = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        rb = GetComponent<Rigidbody2D>();
        
        startpos = transform.position;
        target = GameObject.Find("Player");
        previousPosition = transform.position;
    }

    void Update()
    {
        if (target == null)
            return;
        if (isAttacking)
            return;

        distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        if (distance < range)
        {
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip(); // Funkcja obracaj¹ca AI
            }

            if (distance > attackDistance)
            {
                // PodejdŸ do gracza
                MoveTowardsTarget(direction);
            }
            else if (Time.time >= nextAttack)
            {
                // Atakuj i odejdŸ minimalnie
                StartCoroutine(AttackAndRetreat(direction));
            }
        }
        else
        {
            // Wracaj do pozycji startowej
            MoveTowardsStartPos();
        }

        // Oblicz prêdkoœæ
        Vector2 currentPosition = transform.position;
        float speedValue = (currentPosition - previousPosition).magnitude / Time.deltaTime;
        previousPosition = currentPosition;

        // Ustaw parametr Speed w animatorze
       
    }

    void MoveTowardsTarget(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
    }

    void MoveTowardsStartPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, startpos, speed * Time.deltaTime);
      
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator AttackAndRetreat(Vector3 direction)
    {
        isAttacking = true;

        
        yield return new WaitForSeconds(0.25f);
        

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firepoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            HealthManager healthManager = enemy.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
                Debug.Log("We hit " + enemy.name);
            }
        }

        // Odejœcie minimalne po ataku
        Vector3 retreatDirection = -direction;
        float retreatDistance = 1f;
        Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;

        

        // Smoothly move towards the retreat position
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, retreatPosition);
        float retreatSpeed = speed; // Adjust the speed if necessary

        // Set animator to running
        

        while (Vector3.Distance(transform.position, retreatPosition) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * retreatSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, retreatPosition, fractionOfJourney);

            // Update the speed parameter during the retreat
            speed = 1f;
            

            yield return null;
        }

        // Reset the speed parameter after retreat
        

        nextAttack = Time.time + cooldown;
        isAttacking = false;
    }



    private void OnDrawGizmos()
    {
        if (firepoint != null)
        {
            Gizmos.DrawWireSphere(firepoint.position, attackRange);
        }
    }
}
