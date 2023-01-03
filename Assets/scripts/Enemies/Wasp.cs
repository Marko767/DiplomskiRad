using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;
    private float Speed = 1;

    private int MaxHP = 25;
    private int currentHP;

    public HpBAR hpBAR;

    private int damage = 1;

    // Update is called once per frame
    void Update()
    {
        hpBAR.SetHealth(currentHP);

        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        
        if(distanceToPlayer < 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        }
    }

    void Start()
    {
        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {   
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    }

    public void LocatePlayer(GameObject pl)
    {
        player = pl;
    }
}
