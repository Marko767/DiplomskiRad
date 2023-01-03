using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public float attackRange = 2f;
    public int damage = 20;
    public LayerMask enemiesLayer;

    public int MaxHP = 100;
    public int currentHP;

    public int MaxEnergy = 100;
    public int currentEnergy;

    public HpBAR hpBAR;
    public HpBAR eBAR;

    public GameObject UltiBall;

    public SpriteRenderer sprite;

    public Playerinfo playerinfo;

    private float immCounter;
    private bool immUsed;

    public PlayerAni animationControl;

    private void Start()
    {
        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);

        currentEnergy = 0;
        eBAR.SetMaxHP(MaxEnergy);

        immCounter = 10f;
        immUsed = false;

        MaxHP += (int)(0.1 * playerinfo.playerstats.HP);
        if (playerinfo.playerstats.HPtalents >= 2)
        {
            MaxHP += (int)(0.05 * playerinfo.playerstats.HP);
        }
        
        damage += (int)(0.1 * playerinfo.playerstats.ATK);
        if (playerinfo.playerstats.ATKtalents >= 2)
        {
            damage += (int)(0.05 * playerinfo.playerstats.ATK);
        }
    }

    void Update()
    {
        hpBAR.SetHealth(currentHP);
        eBAR.SetHealth(currentEnergy);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentEnergy == 100)
        {
            UltimateAbillity();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsePotion();
        }

        if (immCounter > 0 && immCounter != 10f)
        {
            immCounter -= Time.deltaTime;
            if (immCounter <= 0)
            {
                immCounter = 10f;
            }
        }
    }
    private void Attack()
    {
        Collider2D[] hitEnemies;

        if(playerinfo.playerstats.ATKtalents >= 3)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemiesLayer);
            hitEnemies = hitEnemies.Concat(Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemiesLayer)).ToArray();
        }
        else if (transform.localScale.x > 0)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemiesLayer);
        }
        else
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemiesLayer);
        }
        animationControl.SetCharacterState("attack");


        foreach (Collider2D enemy in hitEnemies)
        {
            InflictDamage(enemy);
        }
    }

    public void TakeDamage(int damage)
    {
        if ((currentHP < (0.3 * MaxHP)) && (playerinfo.playerstats.HPtalents >= 3) && (immUsed == false))
        {
            immCounter = 2f;
            immUsed = true;
        }
        else if (immCounter == 10f)
        {
            currentHP -= damage;
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //SceneManager.LoadScene("RespawnMenu");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
    }

    private void InflictDamage(Collider2D enemy)
    {
        currentEnergy += 20;
        if (currentEnergy > MaxEnergy)
        {
            currentEnergy = MaxEnergy;
        }

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

        else if (enemy.name == "TroggBoss")
        {
            enemy.GetComponent<TroggBoss>().TakeDamage(damage);
        }
    }

    private void UltimateAbillity()
    {
        currentEnergy = 0;
        animationControl.SetCharacterState("ulti");
        GameObject ulti = Instantiate(UltiBall, transform.position, Quaternion.identity);
        UltimateBall ball = ulti.GetComponent<UltimateBall>();
        ball.SetDestination(transform.localScale);
    }

    private void UsePotion()
    {
       if(playerinfo.playerstats.noPotions > 0)
        {
            playerinfo.playerstats.noPotions -= 1;
            if (playerinfo.playerstats.HPtalents >= 1)
            {
                currentHP += 25;
            }
            else
            {
                currentHP += 20;
            } 
        }
    }
}
