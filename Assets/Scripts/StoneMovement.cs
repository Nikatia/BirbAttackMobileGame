using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneMovement : MonoBehaviour
{
    private float speed;
    Rigidbody rb;


    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "1stChallenge" || sceneName == "2ndChallenge")
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
