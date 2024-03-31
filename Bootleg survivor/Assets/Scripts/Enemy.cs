using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 0.5f;
    public float max_health = 100f;
    public float health = 1f;
    public float speed = 1f;
    private const int speed_scaler = 100;

    void Awake()
    {
        State.instance.EnemyList.Add(this);
        health = max_health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move to player
        if (State.instance.player == null)
        {
            Debug.Log("Player not found");

            // Destroy self
            Die();
        }

        Vector3 direction = State.instance.player.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed / speed_scaler;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        // Update darkness of sprite
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.r = Mathf.Lerp(0, color.r, health / max_health);
        color.g = Mathf.Lerp(0, color.g, health / max_health);
        color.b = Mathf.Lerp(0, color.b, health / max_health);
        sprite.color = color;
    }

    public void Die()
    {
        State.instance.EnemyList.Remove(this);
        Destroy(gameObject);
    }
}
