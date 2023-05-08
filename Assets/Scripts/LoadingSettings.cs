using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSettings : MonoBehaviour
{
    public GameObject birb;
    private float savedVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            savedVolume = PlayerPrefs.GetFloat("volume");
            birb.GetComponent<AudioSource>().volume = savedVolume;
            GetComponent<AudioSource>().volume = savedVolume;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "1stChallenge")
        {
            Time.timeScale = 0;
        }
    }
}
