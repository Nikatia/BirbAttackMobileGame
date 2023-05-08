using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawn = GameObject.Find("Spawn");
        analytics = GameObject.Find("Analytics");
        items = GameObject.Find("Items");
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

                ////plays hit animation and destroys birb
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
        }
    }
}

