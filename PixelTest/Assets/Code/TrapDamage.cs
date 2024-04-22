using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var healthManager = collision.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }
        }
    }
}