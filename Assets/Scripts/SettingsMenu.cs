using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //Not in use at the moment

    public GameObject birb;
    public GameObject cameraForAudio;
    public GameObject slider;

    private float savedVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            savedVolume = PlayerPrefs.GetFloat("volume");
            slider.GetComponent<Slider>().value = savedVolume;
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
        birb.GetComponent<AudioSource>().volume = volume;
        cameraForAudio.GetComponent<AudioSource>().volume = volume;
    }
}
