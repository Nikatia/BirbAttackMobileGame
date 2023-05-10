using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirbInteractionController : MonoBehaviour
{
    Animator anim;
    private GameObject spawn;
    private GameObject analytics;
    private GameObject items;
    private float birbSpeed;
    private float deathPositionX;
    private float deathPositionY;
    private float deathPositionZ;
    private int leftOrRight;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawn = GameObject.Find("Spawn");
        analytics = GameObject.Find("Analytics");
        items = GameObject.Find("Items");
        leftOrRight = Random.Range(0, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaseInteractable>() != null)
        {
            other.gameObject.GetComponent<BaseInteractable>().OnEnterInteract();
            birbSpeed = this.GetComponent<BirbMovement>().speed;

            //if birb crashes to the house
            if (other.CompareTag("House"))
            {
                //launches analytics script: method for birb, that reached the house. Includes info about birb's speed.
                analytics.GetComponent<Analytics>().BirbReachedHouse(birbSpeed);

                //plays hit animation and destroys birb
                anim.Play("PoofHouse");
                this.GetComponent<BirbMovement>().speed = 0;
                Destroy(this.gameObject, 1f);

                //adds a birb to counting in Win script, which later checks winning conditions
                spawn.GetComponent<Win>().AddBirb();
            }

            //if birb is hit with the stone
            if (other.CompareTag("Stone"))
            {
                deathPositionX = this.transform.position.x;
                deathPositionY = this.transform.position.y;
                deathPositionZ = this.transform.position.z;

                //launches analytics script: method for killed birb. Includes info about birb's speed.
                analytics.GetComponent<Analytics>().BirbKilled(birbSpeed);

                //plays hit animation and destroys both birb and the stone
                anim.Play("Poof");
                this.GetComponent<BirbMovement>().speed = 0;
                Destroy(this.gameObject, 1f);
                Destroy(other.gameObject);

                //adds a birb to counting in Win script, which later checks winning conditions
                spawn.GetComponent<Win>().AddBirb();

                items.GetComponent<Items>().ItemRoulette(deathPositionX, deathPositionY, deathPositionZ);
            }

            //if birb encounters a kite
            if (other.CompareTag("Kite"))
            {
                //turns left or right depending on randomly generated number at start
                if (leftOrRight <= 3)
                {
                    transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                }
                else
                {
                    transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                }
            }

            //if birb passed the house without colliding with it
            if (other.CompareTag("OuterCollider"))
            {
                Destroy(this.gameObject);

                //adds a birb to counting in Win script, which later checks winning conditions
                spawn.GetComponent<Win>().AddBirb();
            }
        }
        else
        {
            //Debug.Log("Collided with NOT Interactable " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BaseInteractable>() != null)
        {
            other.gameObject.GetComponent<BaseInteractable>().OnExitInteract();

            if (other.CompareTag("Kite"))
            {
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            }
        }
    }
}

