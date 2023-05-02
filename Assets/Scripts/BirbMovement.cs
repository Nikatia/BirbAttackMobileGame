using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BirbMovement : MonoBehaviour
{
    public float speed;
    private bool isUp = true;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5f, 35f);
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
