using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public float spawn_period;
    private float next_spawn_time = 0;
    private int spawn_count = 0;

    private void Update()
    {
        if(State.instance.player.health <= 0){
            return;
        }
        if (next_spawn_time < Time.time)
        {
            next_spawn_time = Time.time + spawn_period;
            Spawn();
        }
    }

    private void Spawn()
    {
        spawn_count++;
        if (spawn_count % 10 == 0)
        {
            spawn_period -= 0.1f;
            spawn_period = Mathf.Max(0.5f, spawn_period);
        }

        GameObject EnemyPrefab = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)];
        if (EnemyPrefab == null)
        {
            Debug.LogError("Spawner EnemyPrefab is null");
            return;
        }

        float distance = Random.Range(12, 15);
        float angle = Random.Range(0, 360);
        float x = State.instance.player.transform.position.x + distance * Mathf.Cos(angle);
        float y = State.instance.player.transform.position.y + distance * Mathf.Sin(angle);
        Instantiate(EnemyPrefab, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Enemy>();
    }
}
