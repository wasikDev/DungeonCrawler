using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRanger : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint; // Punkt, z którego bêd¹ wystrzeliwane pociski
    public Transform player;
    public float shootingRange = 10.0f;
    public float shootInterval = 1.5f;
    public float moveSpeed = 5.0f;
    public float minDistanceFromPlayer = 5.0f;

    private float shootTimer;

    void Update()
    {
        Vector2 directionToPlayer = (Vector2)(player.position - transform.position);
        float distanceToPlayer = directionToPlayer.magnitude;

        // Rotate towards the player - horizontal axis only
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        // Movement logic
        if (distanceToPlayer > shootingRange)
        {
            // Move closer if too far
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer < minDistanceFromPlayer)
        {
            // Move away if too close
            Vector2 moveAwayDirection = (Vector2)transform.position - directionToPlayer;
            transform.position = Vector2.MoveTowards(transform.position, moveAwayDirection, moveSpeed * Time.deltaTime);
        }

        // Shooting logic
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval && distanceToPlayer <= shootingRange)
        {
            Shoot(directionToPlayer.normalized);
            shootTimer = 0;
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 20f; // Adjust speed as necessary
    }
}

