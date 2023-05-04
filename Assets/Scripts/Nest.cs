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
    public bool nestDone;
    public int nestGrowth;

    private Vector3 nestScale, maxNest;
    private AudioSource camAudio;
    private GameObject[] birbs;
    

    // Start is called before the first frame update
    void Start()
    {
        maxNest = new Vector3(500, 500, 1000);

        if (saveData == null)
        {
            saveData = GameObject.Find("SaveData");
        }

        nestGrowth = saveData.GetComponent<NoDestroyData>().nestGrowth;
        nestScale = new Vector3(500, 500, nestGrowth);
        transform.localScale = nestScale;

        nestDone = false;
        camAudio = cameraForSounds.GetComponent<AudioSource>();
    }

    public void NestScaling()
    {
        if (!nestScale.Equals(maxNest))
        {
            Debug.Log("Grow");
            nestGrowth = nestGrowth + 200;
            nestScale = new Vector3(500, 500, nestGrowth);
            transform.localScale = nestScale;
            saveData.GetComponent<NoDestroyData>().NestGrowthSave();
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
}
