using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        if (collision.gameObject.tag == "Player")
        {
            var target = collision.gameObject.GetComponent<CharacterController2D>();
            target.TakeDamage(damage);
=======
        if (collision.tag == "Player")
        {
            var healthManager = collision.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }
>>>>>>> Stashed changes
        }

        if (collision.gameObject.tag == "Enemy")
        {
            var target = collision.gameObject.GetComponent<AiController>();
            target.TakeDamage(damage);
        }


    }
}
