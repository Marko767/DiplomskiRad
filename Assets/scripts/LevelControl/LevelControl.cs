using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public GameObject player;

    public Transform[] SpawnLocations;
    public GameObject Chest;
    public GameObject Portal;
    public GameObject[] EnemiesPool;

    private List<GameObject> EnemiesAlive = new List<GameObject>();

    private bool chestdestroyed = false;

    private void Start()
    {
        foreach(Transform position in SpawnLocations)
        {
            SpawnEnemy(position);
        }
    }

    private void Update()
    {
        bool check = true;
        foreach (GameObject obj in EnemiesAlive)
        {       
            if(obj != null)
            {
                check = false;
            }
            
        }
        if (check == true && chestdestroyed == false)
        {
            Debug.Log("vicoty");
            Chest.SetActive(true);
            Portal.SetActive(true);
            chestdestroyed = true;
        }
    }

    private void SpawnEnemy(Transform position)
    {
        int random = Random.Range(0, EnemiesPool.Length);
        //int random = Random.Range(0, 3);
        GameObject enemy = Instantiate(EnemiesPool[random], position.position, Quaternion.identity);
        EnemiesAlive.Add(enemy);

        if (enemy.name == "Blob" || enemy.name == "Blob(Clone)")
        {
            enemy.GetComponent<Blob>().player = player;
        }

        else if (enemy.name == "Wasp" || enemy.name == "Wasp(Clone)")
        {
            enemy.GetComponent<Wasp>().player = player;
        }

        else if (enemy.name == "Archer" || enemy.name == "Archer(Clone)")
        {
            enemy.GetComponent<Archer>().player = player;
        }

        else if (enemy.name == "Crow" || enemy.name == "Crow(Clone)")
        {
            enemy.GetComponent<Crow>().player = player;
        }

        else if (enemy.name == "Duelist" || enemy.name == "Duelist(Clone)")
        {
            enemy.GetComponent<Duelist>().player = player;
        }

    }
}
