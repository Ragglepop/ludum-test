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
    private Vector3 start_position;

    void Awake()
    {
        State.instance.ProjectilesList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Projectile created");
        birth_time = Time.time;
        start_angle = Random.Range(0, 2 * Mathf.PI);
        distance = 0f;
        start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - birth_time > 5)
        {
            die();
        }

        float age = Time.time - birth_time;
        distance = age * speed;
        float angle = start_angle + age * speed;
        float x = start_position.x + distance * Mathf.Cos(angle);
        float y = start_position.y + distance * Mathf.Sin(angle);
        transform.position = new Vector3(x, y, 0);
        transform.Rotate(0, 0, speed);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void die()
    {
        State.instance.ProjectilesList.Remove(this);
        Destroy(gameObject);
    }
}
