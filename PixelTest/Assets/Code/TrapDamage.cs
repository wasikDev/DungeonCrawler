using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 25; // Example damage value

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var target = collision.gameObject.GetComponent<CharacterController2D>();
            target.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            var target = collision.gameObject.GetComponent<AiController>();
            target.TakeDamage(damage);
        }


    }
}
