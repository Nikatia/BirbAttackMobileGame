using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BirbInteractionController : MonoBehaviour
{
    public GameObject nest;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            }

            if (other.CompareTag("Stone"))
            {
                anim.Play("Poof");
                this.GetComponent<BirbMovement>().speed = 0;
                Destroy(this.gameObject, 1f);
                Destroy(other.gameObject);
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

