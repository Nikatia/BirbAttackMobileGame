using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameButtons : MonoBehaviour
{
    public GameObject craftGroup;
    public GameObject craftOpenButton;
    public GameObject cameraForAudio;

    public void PauseGame()
    {
        Time.timeScale = 0;
        cameraForAudio.GetComponent<AudioSource>().Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void CraftOpen()
    {
        StartCoroutine(CraftOpenAndPause());
    }

    IEnumerator CraftOpenAndPause()
    {
        craftGroup.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        PauseGame();
        craftOpenButton.gameObject.SetActive(false);
    }


}
