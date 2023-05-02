using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections;
using UnityEngine;

public class BirbInteractionController : MonoBehaviour
{
    public GameObject nest;
    Animator anim;
    private GameObject spawn;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawn = GameObject.Find("Spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaseInteractable>() != null)
        {
            other.gameObject.GetComponent<BaseInteractable>().OnEnterInteract();

            if (other.CompareTag("House"))
            {
                anim.Play("PoofHouse");
                this.GetComponent<BirbMovement>().speed = 0;
                Destroy(this.gameObject, 1f);
                spawn.GetComponent<Win>().AddBirb();
            }

            if (other.CompareTag("Stone"))
            {
                anim.Play("Poof");
                this.GetComponent<BirbMovement>().speed = 0;
                Destroy(this.gameObject, 1f);
                Destroy(other.gameObject);
                spawn.GetComponent<Win>().AddBirb();
            }
        }
        else
        {
            Debug.Log("Collided with NOT Interactable " + other.name);
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

