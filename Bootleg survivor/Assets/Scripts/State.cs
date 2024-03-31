using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public static State instance; //Singleton
    public Player player;
    public List<Enemy> EnemyList;
    public List<Projectile> ProjectilesList;
    public int Score;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void IncrementScore(int amount){
        Score += amount;
    }
}
