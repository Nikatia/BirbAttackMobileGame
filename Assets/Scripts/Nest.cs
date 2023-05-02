using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    private Vector3 nestScale, maxNest;
    private int nestGrowth;
    public GameObject eggs;

    // Start is called before the first frame update
    void Start()
    {
        nestScale = new Vector3(500, 500, 0);
        maxNest = new Vector3(500, 500, 1000);
        nestGrowth = 0;
        transform.localScale = nestScale;
    }

    public void NestScaling()
    {
        if (!nestScale.Equals(maxNest))
        {
            nestGrowth = nestGrowth + 200;
            nestScale = new Vector3(500, 500, nestGrowth);
            transform.localScale = nestScale;
        }
        else
        {
            eggs.SetActive(true);
        }
    }
}
