using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestroyData : MonoBehaviour
{
    public int nestGrowth, sticks, strings;
    
    public bool speederOn, kiteOn, netOn;

    private GameObject nest;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        sticks = 0;
        strings = 0;
        NestGrowthReset();
        speederOn = false;
        kiteOn = false;
        netOn = false;
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

    public void AddStick()
    {
        sticks++;
    }

    public void AddString() 
    {
        strings++;
    }

    public void TurnOnSpeeder()
    {
        speederOn = true;
    }

    public void TurnOnKite()
    {
        kiteOn = true;
    }

    public void TurnOnNet()
    {
        netOn = true;
    }
}
