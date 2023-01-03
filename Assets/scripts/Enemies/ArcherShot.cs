using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShot: MonoBehaviour
{
    private int damage;
    private Vector3 targetDir;
    private int speed = 5;

    public void SetDamageAndTarget(int SetDMG, Vector3 dir)
    {
        damage = SetDMG;
        targetDir = dir;
    }

    private void Update()
    {
        gameObject.transform.position += targetDir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Tilemap" || collision.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
