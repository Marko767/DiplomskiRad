using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private int damage;
    private Vector3 targetDir;

    public void SetDamageAndTarget(int SetDMG, Vector3 dir)
    {
        damage = SetDMG;
        targetDir = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Boss")
        {
            //targetDir = new Vector3(0, 0, 0);
        }
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
