using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestroyData : MonoBehaviour
{
    public int nestGrowth;
    private GameObject nest;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        NestGrowthReset();
        
    }

    public void NestGrowthSave()
    {
        nestGrowth = nestGrowth + 200;
    }

    public void NestGrowthReset()
    {
        nestGrowth = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "MainMenu")
        {
            nest = GameObject.Find("Nest");
            nest.GetComponent<Nest>().CleanNest();
        }
    }
}
