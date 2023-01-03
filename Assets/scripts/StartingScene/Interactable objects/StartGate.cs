using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGate : MonoBehaviour
{
    public Canvas interactNotification;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Debug.Log("play");
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
            int random = Random.Range(1, 4);
            Debug.Log("Level_1_" + random.ToString());
            SceneManager.LoadScene("Level_1_" + random.ToString());
        }
    }
}
