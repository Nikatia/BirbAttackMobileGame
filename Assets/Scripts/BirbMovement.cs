using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BirbMovement : MonoBehaviour
{
    public float speed;

    private float maxSpeed;
    private float minSpeed;

    private bool isUp = true;

    // Start is called before the first frame update
    void Start()
    {
        //setting up bird's speed as random number between max and min speed
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "1stChallenge")
        {
            maxSpeed = 20f;
            minSpeed = 10f;
        }
        else if (sceneName == "2ndChallenge")
        {
            maxSpeed = 25f;
            minSpeed = 15f;
        }
        else if (sceneName == "3rdChallenge")
        {
            maxSpeed = 28f;
            minSpeed = 18f;
        }
        else if (sceneName == "4thChallenge" || sceneName == "5thChallenge")
        {
            maxSpeed = 33f;
            minSpeed = 23f;
        }
        else if (sceneName == "6thChallenge")
        {
            maxSpeed = 36f;
            minSpeed = 26f;
        }
        else if (sceneName == "7thChallenge")
        {
            maxSpeed = 36f;
            minSpeed = 26f;
        }
        speed = Random.Range(minSpeed, maxSpeed);

        //toggles between up and down, which is used in update to change direction of additional movement
        StartCoroutine(UpAndDown());
    }

    // Update is called once per frame
    void Update()
    {
        //moves forward with setted at start speed
        transform.position += transform.forward * speed * Time.deltaTime;

        //moves up or down depending on bool, that is set in coroutine
        if (speed != 0)
        {
            if (isUp)
            {
                transform.position += -transform.up * speed * Time.deltaTime;
            }
            else
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
        }
    }

    IEnumerator UpAndDown()
    {
        isUp = true;//birb now moves up
        yield return new WaitForSeconds(1f);
        isUp = false; //birb now moves down
        yield return new WaitForSeconds(1f);
        StartCoroutine(UpAndDown());
    }
}
