using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] Enemies;

    public float spawn_period;


    private float last_spawn_time = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Time.time - last_spawn_time > spawn_period)
        {
            last_spawn_time = (float)Time.time;
            Spawn();
        }
    }

    private void Spawn()
    {
        if (Enemies.Length == 0)
        {
            Debug.Log("No enemies to spawn");
            return;
        }

        int enemy_index = Random.Range(0, Enemies.Length);
        GameObject enemy = Enemies[enemy_index];

        float x = player.transform.position.x + Random.Range(-10, 10);
        float y = player.transform.position.y + Random.Range(-10, 10);

        Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
        enemy.GetComponent<EnemyAI>().player = player;
    }
}
