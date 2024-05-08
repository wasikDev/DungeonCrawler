using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerScript : MonoBehaviour
{
    public int damage = 10;
    public float pushBackForce = 5f;

   
    // Obs�uga zdarze� trigger - wej�cie w obszar ataku
    private void OnTriggerEnter2D(Collider2D other)
    {
        AttackEnemy(other);
    }

    // Obs�uga zdarze� trigger - pozostanie w obszarze ataku
    private void OnTriggerStay2D(Collider2D other)
    {
        AttackEnemy(other);
    }

    // Zadawanie obra�e� i odpychanie przeciwnik�w
    private void AttackEnemy(Collider2D enemy)
    {
        HealthManager healthManager = enemy.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
            Debug.Log($"Flamethrower hit {enemy.name} for {damage} damage.");
        }

        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 pushDirection = (enemy.transform.position - transform.position).normalized;
            rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
            Debug.Log($"Flamethrower pushed {enemy.name} with force {pushBackForce}.");
        }
    }
}
