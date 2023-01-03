using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    public Canvas interactNotification;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            interactNotification.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            interactNotification.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (interactNotification.gameObject.activeInHierarchy == true && Input.GetKeyDown(KeyCode.F))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        string[] sceneInfo = scene.name.Split("_");
        Debug.Log(sceneInfo[1]);

        if(sceneInfo[1] == "5")
        {
            int random = Random.Range(1, 3);
            Debug.Log("BossLevel_" + random.ToString());
            SceneManager.LoadScene("BossLevel_" + random.ToString());
        }
        else if(sceneInfo[0] == "BossLevel") 
        {
            int random = Random.Range(1, 4);
            Debug.Log("Level_1_" + random.ToString());
            SceneManager.LoadScene("Level_1_" + random.ToString());
        }
        else
        {
            int random = Random.Range(1, 4);
            Debug.Log("Level_" + (int.Parse(sceneInfo[1]) + 1) + "_" + random.ToString());
            SceneManager.LoadScene("Level_" + (int.Parse(sceneInfo[1]) + 1) + "_" + random.ToString());
        }
    }
}
