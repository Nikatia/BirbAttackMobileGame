using System.Collections;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public GameObject eggs;
    public GameObject cameraForSounds;
    public AudioClip failClip;
    public GameObject failUI;
    public GameObject spawn;
    public bool nestDone;
    public int nestGrowth;

    private Vector3 nestScale, maxNest;
    private AudioSource camAudio;
    private GameObject[] birbs;
    

    // Start is called before the first frame update
    void Start()
    {
        nestScale = new Vector3(500, 500, 0);
        maxNest = new Vector3(500, 500, 1000);
        nestGrowth = 0;
        transform.localScale = nestScale;
        nestDone = false;
        camAudio = cameraForSounds.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (cameraForSounds == null)
        {
            AssignAgain();
        }
    }

    public void NestScaling()
    {
        if (!nestScale.Equals(maxNest))
        {
            nestGrowth = nestGrowth + 200;
            nestScale = new Vector3(500, 500, nestGrowth);
            transform.localScale = nestScale;
        }
        else
        {
            eggs.SetActive(true);
            nestDone = true;
        }

        if (nestDone)
        {
            camAudio.clip = failClip;
            camAudio.Play();
            failUI.SetActive(true);
            spawn.SetActive(false);
            birbs = GameObject.FindGameObjectsWithTag("Birb");
            if (birbs != null)
            {
                foreach (GameObject birb in birbs)
                {
                    Destroy(birb.gameObject);
                }
            }
        }
    }

    public void CleanNest()
    {
        nestGrowth = 0;
    }

    public void AssignAgain()
    {
        cameraForSounds = GameObject.Find("Main Camera");
        failUI = GameObject.Find("FailUI");
        spawn = GameObject.Find("Spawn");
    }
}
