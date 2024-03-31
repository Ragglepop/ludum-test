using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 1f;
    private float distance;
    private float birth_time;
    private float start_angle;
    private int direction = 1;

    void Awake()
    {
        State.instance.ProjectilesList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        birth_time = Time.time;
        start_angle = Random.Range(0, 2 * Mathf.PI);
        distance = 0f;

        // Called here to avoid a frame where the projectile is not in the right position
        UpdatePosition();
    }

    // Set the direction of the projectile
    public void SetDirection(int dir)
    {
        if (dir >= 0) {
            direction = 1;
        } else {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
    
    void UpdatePosition()
    {
        if (Time.time - birth_time > 5)
        {
            Die();
        }

        float age = Time.time - birth_time;
        distance = 1 + age * speed / 3;
        float angle = start_angle + direction * age * speed;
        float x = State.instance.player.transform.position.x + distance * Mathf.Cos(angle);
        float y = State.instance.player.transform.position.y + distance * Mathf.Sin(angle);
        // Setting rotation an position of the projectile
        // Projectile will move in a circle around the player, always facing the player
        transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.Euler(Mathf.Rad2Deg * angle * Vector3.forward));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(damage);
        }
    }

    void Die()
    {
        State.instance.ProjectilesList.Remove(this);
        Destroy(gameObject);
    }
}
