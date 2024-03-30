using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public static State _; //Singleton
    public Player player;
    public List<EnemyAI> EnemyList;


    // Start is called before the first frame update
    void Awake()
    {
        _ = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
