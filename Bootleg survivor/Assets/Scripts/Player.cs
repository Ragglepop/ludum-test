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
    private int enemy_collider_count = 0;


    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (min_health <= 0)
        {
            Debug.Log("Player is dead");
        } 
        else if (enemy_collider_count > 0)
        {
            health -= damage_rate;
            Debug.Log("Player health: " + health);
        }
        else if (health < max_health) {
            health += regen_rate;
            Debug.Log("Player health: " + health);
        }

        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        if (next_projectile_spawn < Time.time)
        {
            GameObject projectile = Instantiate(weapon, transform.position, Quaternion.identity);
            next_projectile_spawn = Time.time + projectile_spawn_period;
        }
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            enemy_collider_count++;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            enemy_collider_count--;
        }
    }

}
