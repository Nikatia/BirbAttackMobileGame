using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneMovement : MonoBehaviour
{
    private bool speederOn;
    private GameObject saveData;
    private float speed;
    Rigidbody rb;


    void Start()
    {
        if (saveData == null)
        {
            saveData = GameObject.Find("SaveData"); //data, which is not destroyed inbetween scenes
        }

        //check in saved data if speeder is installed
        speederOn = saveData.GetComponent<NoDestroyData>().speederOn;

        AdjustStoneSpeed();
    }

    //speed of stone depends if speeder is installed or not
    public void AdjustStoneSpeed()
    {
        if (speederOn == false)
        {
            speed = 45;
        }
        else
        {
            speed = 90;
        }

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        Destroy(this.gameObject, 5f);
    }
}
