using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject weapon;
    public float speed = 1f;
    public float damage_rate = 1;
    public float regen_rate = 1;
    public float projectile_spawn_period = 3.0f;
    public Slider HPSlider;
    private float next_projectile_spawn = 0;
    public float max_health = 100;
    private float min_health = 0;
    public float health = 100;
    private int n_projectiles = 0;
    public static event Action PlayerDied;


    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if(State.instance.player.health <= 0){
            return;
        }
        SpawnProjectile();
        RegenerateHealth();
    }

    void RegenerateHealth()
    {
        if(health < max_health){
            if(health + regen_rate * Time.deltaTime > max_health){
                health = max_health;
            }else{
                health += regen_rate * Time.deltaTime;
            }
        }
        HPSlider.value=health;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, min_health, max_health);

        HPSlider.value=health;

        if (health <= min_health)
        {
            Debug.Log("Player is dead");
            PlayerDied?.Invoke();
        }
    }
}
