using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 25; // Example damage value

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" ) // Make sure your player GameObject is tagged as "Player"
        {
            //Debug.Log("Enemy is close to the player!");
           // CharacterController player = other.GetComponent<CharacterController>();
            var player = collision.GetComponent<CharacterController2D>();
           player.TakeDamage(damage);
        }
    }
}
