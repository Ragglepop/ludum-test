using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;
    public float speed = 1f;
    public float damage_rate = 1;
    public float regen_rate = 1;
    public float projectile_spawn_period = 3.0f;
    private float next_projectile_spawn = 0;
    public float max_health = 100;
    private float min_health = 100;
    private float health = 100;
    private int n_projectiles = 0;


    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnProjectile();
        RegenerateHealth();
    }

    void RegenerateHealth()
    {
        health += regen_rate * Time.deltaTime;
    }

    void SpawnProjectile()
    {
        if (next_projectile_spawn < Time.time)
        {
            GameObject projectile = Instantiate(weapon, transform.position, Quaternion.identity);
            next_projectile_spawn = Time.time + projectile_spawn_period;
            projectile.GetComponent<Projectile>().SetDirection(n_projectiles % 2 == 0 ? 1 : -1);
            n_projectiles++;
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            TakeDamage(enemy.damage);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, min_health, max_health);

        if (health <= min_health)
        {
            Debug.Log("Player is dead");
        }
    }
}
