using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 0.5f;
    public float max_health = 100f;
    public float health = 1f;
    public float speed = 1f;
    public float attackInterval = 0.5f;
    public string Name;
    public int ScoreValue;
    private bool attackingPlayer;
    private const int speed_scaler = 100;

    void Awake()
    {
        State.instance.EnemyList.Add(this);
        health = max_health;
    }

    void Start(){
        if(State.instance.player.health <= 0){
            StopAllCoroutines();
            return;
        }
        StartCoroutine(CheckForAttack());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(State.instance.player.health <= 0){
            return;
        }
        // Move to player
        if (State.instance.player == null)
        {
            Debug.Log("Player not found");

            // Destroy self
            Die();
        }

        Vector3 direction = State.instance.player.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed / speed_scaler;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        // Update darkness of sprite
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.r = Mathf.Lerp(0, color.r, health / max_health);
        color.g = Mathf.Lerp(0, color.g, health / max_health);
        color.b = Mathf.Lerp(0, color.b, health / max_health);
        sprite.color = color;
    }

    public void Die()
    {
        State.instance.EnemyList.Remove(this);
        StopAllCoroutines();
        State.instance.IncrementScore(ScoreValue);

        Destroy(gameObject);
    }

    IEnumerator CheckForAttack(){
        if(attackingPlayer && State.instance.player.health>0)
        {
            State.instance.player.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackInterval);

        StartCoroutine(CheckForAttack());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == State.instance.player.gameObject)
        {
            attackingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == State.instance.player.gameObject)
        {
            attackingPlayer = false;
        }
    }
}
