using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1f;
    private const int speed_scaler = 100;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = State.instance.player.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed / speed_scaler;
    }

}
