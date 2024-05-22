using System.Collections;
using UnityEngine;

public class AiControllerMele : MonoBehaviour
{
    GameObject target;
    public float speed = 1f;
    public float range = 10f;
    float distance;
    private Animator animator;
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
        animator = GetComponent<Animator>();
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
                Flip(); // Funkcja obracaj�ca AI
            }

            if (distance > attackDistance)
            {
                // Podejd� do gracza
                MoveTowardsTarget(direction);
            }
            else if (Time.time >= nextAttack)
            {
                // Atakuj i odejd� minimalnie
                StartCoroutine(AttackAndRetreat(direction));
            }
        }
        else
        {
            // Wracaj do pozycji startowej
            MoveTowardsStartPos();
        }

        // Oblicz pr�dko��
        Vector2 currentPosition = transform.position;
        float speedValue = (currentPosition - previousPosition).magnitude / Time.deltaTime;
        previousPosition = currentPosition;

        // Ustaw parametr Speed w animatorze
        animator.SetFloat("Speed", speedValue);
    }

    void MoveTowardsTarget(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        animator.SetFloat("Speed", speed);
    }

    void MoveTowardsStartPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, startpos, speed * Time.deltaTime);
        animator.SetFloat("Speed", speed);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator AttackAndRetreat(Vector3 direction)
    {
        isAttacking = true;

        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Attack", false);

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

        // Odej�cie minimalne po ataku
        Vector3 retreatDirection = -direction;
        float retreatDistance = 1f;
        Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;

        yield return new WaitForSeconds(0.5f); // Ma�a przerwa przed odej�ciem

        // Smoothly move towards the retreat position
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, retreatPosition);
        float retreatSpeed = speed; // Adjust the speed if necessary

        // Set animator to running
        animator.SetFloat("Speed", retreatSpeed);

        while (Vector3.Distance(transform.position, retreatPosition) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * retreatSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, retreatPosition, fractionOfJourney);

            // Update the speed parameter during the retreat
            speed = 1f;
            animator.SetFloat("Speed", speed);

            yield return null;
        }

        // Reset the speed parameter after retreat
        animator.SetFloat("Speed", 0);

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
