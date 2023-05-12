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
    public GameObject saveData;
    public GameObject analytics;
    public GameObject sling;
    public bool nestDone;
    public int nestGrowth;

    private Vector3 nestScale, maxNest;
    private AudioSource camAudio;
    private GameObject[] birbs;
    

    // Start is called before the first frame update
    void Start()
    {
        //when nest is ready
        maxNest = new Vector3(500, 500, 1000);

        if (saveData == null)
        {
            saveData = GameObject.Find("SaveData"); //data, which is not destroyed inbetween scenes
        }

        //check what is saved nest growth stage and apply
        nestGrowth = saveData.GetComponent<NoDestroyData>().nestGrowth;
        nestScale = new Vector3(500, 500, nestGrowth);
        transform.localScale = nestScale;

        nestDone = false;
        camAudio = cameraForSounds.GetComponent<AudioSource>();
    }

    public void NestScaling()
    {
        if (!nestScale.Equals(maxNest)) //if nest is still not ready for the eggs
        {
            //Debug.Log("Grow");
            nestGrowth = nestGrowth + 200;
            nestScale = new Vector3(500, 500, nestGrowth);
            transform.localScale = nestScale; 
            saveData.GetComponent<NoDestroyData>().NestGrowthSave();
        }
        else //if nest is prepared for the eggs
        {
            eggs.SetActive(true);
            nestDone = true;
        }

        if (nestDone) //game over
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
            sling.GetComponent<Shooting>().enabled = false;
            //launches analytics script: level failed method.
            analytics.GetComponent<Analytics>().LevelFailed();
        }
    }

    public void CleanNest() //nest reset back to 0
    {
        nestGrowth = 0;
        nestScale = new Vector3(500, 500, nestGrowth);
        transform.localScale = nestScale;
    }
}
