using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health = 100f;
    public float speed = 1f;
    private const int speed_scaler = 100;

    void Awake()
    {
        State.instance.EnemyList.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move to player
        if (State.instance.player == null)
        {
            Debug.Log("Player not found");

            // Destroy self
            die();
        }

        Vector3 direction = State.instance.player.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed / speed_scaler;
    }

    public void TakeDamage(float damage)
    {
        max_health -= damage;
        if (max_health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        State.instance.EnemyList.Remove(this);
        Destroy(gameObject);
    }
}
