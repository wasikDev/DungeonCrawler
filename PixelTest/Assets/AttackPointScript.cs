using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointScript : MonoBehaviour
{
    public int damage = 25; // Example damage value
    public float cooldown;
    private float nextShot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Time.time > nextShot) 
        {
           
            var player = collision.GetComponent<HealthManager>();
            player.TakeDamage(damage);
            nextShot = Time.time+cooldown;
        }
    }
}
