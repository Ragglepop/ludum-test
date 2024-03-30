using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public List<GameObject> EnemyList;
    public float spawn_period;
    private float last_spawn_time = 0;

    private void Update()
    {
        if (Time.time - last_spawn_time > spawn_period)
        {
            last_spawn_time = (float)Time.time;
            Spawn();
        }
    }

    private void Spawn()
    {
        float x = State.instance.player.transform.position.x + Random.Range(-10, 10);
        float y = State.instance.player.transform.position.y + Random.Range(-10, 10);
        State.instance.EnemyList.Add(Instantiate(EnemyPrefab, new Vector3(x, y, 0), Quaternion.identity).GetComponent<EnemyAI>());
    }
}
