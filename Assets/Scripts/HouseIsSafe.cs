using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseIsSafe : MonoBehaviour
{
    public GameObject analytics;
    public GameObject net;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (net.gameObject.activeInHierarchy == true)
        {
            Debug.Log("Won by net");
            analytics.GetComponent<Analytics>().WonByNet();
        }
        if (sceneName == "7thChallenge" && net.gameObject.activeInHierarchy == false)
        {
            Debug.Log("Won by determination");
            analytics.GetComponent<Analytics>().WonByDetermination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) //redirect to Main Menu on touch
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
