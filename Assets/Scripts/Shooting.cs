using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Shooting : MonoBehaviour
{
    public GameObject sling;
    public GameObject stone;
    Animator anim;
    public GameObject analytics;

    private GameObject saveData;
    private float rotatespeed = 50f;
    private float startingPositionY;
    private float startingPositionX;
    private float firstWait, secondWait;

    private bool rdy;
    private bool speederOn;


    private void Start()
    {
        if (saveData == null)
        {
            saveData = GameObject.Find("SaveData"); //data, which is not destroyed inbetween scenes
        }

        anim = sling.gameObject.GetComponent<Animator>();
        rdy = true;

        //check if speeder is on
        speederOn = saveData.GetComponent<NoDestroyData>().speederOn;
        AdjustSlingSpeed();
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //when we start touching it saves the initial touch position
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingPositionY = touch.position.y;
                startingPositionX = touch.position.x;
            }

            //when touch has moved it checks into which direction it went and rotate sling accordingly (sling is rotated, thus y and x are swapped)
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if ((startingPositionY > touch.position.y))
                {
                    sling.transform.RotateAround(transform.position, -Vector3.right, rotatespeed * Time.deltaTime);

                }
                else if ((startingPositionY < touch.position.y))
                {
                    sling.transform.RotateAround(transform.position, Vector3.right, rotatespeed * Time.deltaTime);
                }

                if (startingPositionX > touch.position.x)
                {
                    sling.transform.RotateAround(transform.position, Vector3.up, rotatespeed * Time.deltaTime);
                }
                else if (startingPositionY < touch.position.x)
                {
                    sling.transform.RotateAround(transform.position, -Vector3.up, rotatespeed * Time.deltaTime);
                }
            }

            //when touch has ended, shoots
            if (Input.GetTouch(0).phase == TouchPhase.Ended && rdy == true) //shooting only when sling is ready to shoot
            {
                StartCoroutine(Shoot());
                analytics.GetComponent<Analytics>().AddShots();
            }
        }
    }

    //sync animation with shooting
    IEnumerator Shoot()
    {
        rdy = false;
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(firstWait);
        Instantiate(stone, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(secondWait);
        anim.ResetTrigger("Shoot");
        rdy = true;
    }

    public void TurnOnSpeeder()
    {
        speederOn = true;
        AdjustSlingSpeed();
    }

    //slingshot shooting speed depends on speeder crafting status
    public void AdjustSlingSpeed()
    {
        if (speederOn == false)
        {
            anim.speed = 1;
            firstWait = 0.1f;
            secondWait = 1.15f;
        }
        else
        {
            anim.speed = 2;
            firstWait = 0.05f;
            secondWait = 0.575f;
        }
    }

}