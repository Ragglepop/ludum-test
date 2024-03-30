using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 1f;
    private const int speed_scaler = 100;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move to player
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
