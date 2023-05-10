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
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "1stChallenge")
        {
            maxSpeed = 20f;
            minSpeed = 10f;
        }
        if (sceneName == "2ndChallenge")
        {
            maxSpeed = 25f;
            minSpeed = 15f;
        }
        if (sceneName == "3rdChallenge")
        {
            maxSpeed = 30f;
            minSpeed = 20f;
        }
        speed = Random.Range(minSpeed, maxSpeed);

        StartCoroutine(UpAndDown());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
