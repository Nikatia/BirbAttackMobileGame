using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private int randomNumber, sticks, strings;

    // Start is called before the first frame update
    void Start()
    {
        sticks = 0;
        strings = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemRoulette()
    {
        randomNumber = Random.Range(0, 100);

        if (randomNumber <=100 && randomNumber > 65)
        {
            sticks++;
        }
        else if (randomNumber <= 65 && randomNumber > 30)
        {
            strings++;
        }
        else
        {

        }
    }
}
