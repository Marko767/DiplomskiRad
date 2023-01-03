using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateBall : MonoBehaviour
{
    public LayerMask enemiesLayer;
    public int damage = 2;
    public float attackRange = 1.5f;
    public float gravityRange = 1.75f;

    private Vector3 ballDestination;
    public int Speed = 2;
    public int gravitySpeed = 40;

    private float ultiDuration = 4f;
    private float damageCD = 0.1f;
    private float damageCounter;

    private void Start()
    {
        damageCounter = damageCD;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ballDestination, Speed * Time.deltaTime);

        Collider2D[] hitEnemiesGravity = Physics2D.OverlapCircleAll(transform.position, attackRange, enemiesLayer);
        Collider2D[] hitEnemiesDamage = Physics2D.OverlapCircleAll(transform.position, gravityRange, enemiesLayer);

        foreach (Collider2D enemy in hitEnemiesGravity)
        {
            enemy.gameObject.transform.position = Vector3.MoveTowards(enemy.gameObject.transform.position, transform.position, gravitySpeed * Time.deltaTime);
        }

        if (ultiDuration > 0)
        {
            ultiDuration -= Time.deltaTime;
        }
        else if (ultiDuration <= 0)
        {
            Destroy(gameObject);
        }

        if (damageCounter > 0)
        {
            damageCounter -= Time.deltaTime;
        }
        else if (damageCounter <= 0)
        {
            foreach (Collider2D enemy in hitEnemiesDamage)
            {
                enemy.gameObject.transform.position = Vector3.MoveTowards(enemy.gameObject.transform.position, transform.position, gravitySpeed * Time.deltaTime);
                InflictDamage(enemy);
            }
            damageCounter = damageCD;
        }
    }

    private void InflictDamage(Collider2D enemy)
    {
        if (enemy.name == "Blob" || enemy.name == "Blob(Clone)")
        {
            enemy.GetComponent<Blob>().TakeDamage(damage);
        }

        else if (enemy.name == "Wasp" || enemy.name == "Wasp(Clone)")
        {
            enemy.GetComponent<Wasp>().TakeDamage(damage);
        }

        else if (enemy.name == "Archer" || enemy.name == "Archer(Clone)")
        {
            enemy.GetComponent<Archer>().TakeDamage(damage);
        }

        else if (enemy.name == "Crow" || enemy.name == "Crow(Clone)")
        {
            enemy.GetComponent<Crow>().TakeDamage(damage);
        }

        else if (enemy.name == "Boss")
        {
            enemy.GetComponent<WormBoss>().TakeDamage(damage);
        }
        else if (enemy.name == "Duelist" || enemy.name == "Duelist(Clone)")
        {
            enemy.GetComponent<Duelist>().TakeDamage(damage);
        }
    }
    public void SetDestination(Vector2 scale)
    {
        if(scale.x > 0)
        {
            ballDestination = transform.position + new Vector3(4, 0.5f, 0);
        }
        else
        {
            ballDestination = transform.position + new Vector3(-4, 0.5f, 0);
        }
        
    }
}
