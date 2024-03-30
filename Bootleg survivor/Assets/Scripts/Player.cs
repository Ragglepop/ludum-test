using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float health = 100;
    private int enemy_collider_count = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("Player is dead");
        } 
        else if (enemy_collider_count > 0)
        {
            health -= 1;
            Debug.Log("Player health: " + health);
        }
        else if (health < 100) {
            health += 1;
            Debug.Log("Player health: " + health);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy_collider_count++;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy_collider_count--;
        }
    }


}
