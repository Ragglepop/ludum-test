using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1f;
    private const int speed_scaler = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move to player
        GameObject player = GameObject.Find("Triangle");
        if (player == null)
        {
            Debug.Log("Player not found");
            return;
        }

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed / speed_scaler;
    }

}
