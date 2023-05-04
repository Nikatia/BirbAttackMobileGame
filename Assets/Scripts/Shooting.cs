using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    public GameObject sling;
    public GameObject stone;
    Animator anim;
    public GameObject analytics;

    private float rotatespeed = 50f;
    private float startingPositionY;
    private float startingPositionX;

    private bool rdy;

    private void Start()
    {
        anim = sling.gameObject.GetComponent<Animator>();
        rdy = true;
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
            if (Input.GetTouch(0).phase == TouchPhase.Ended && rdy == true)
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
        yield return new WaitForSeconds(0.1f);
        Instantiate(stone, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(1.15f);
        anim.ResetTrigger("Shoot");
        rdy = true;
    }

}