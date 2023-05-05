using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyData : MonoBehaviour
{
    public int nestGrowth;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        NestGrowthReset();
    }

    public void NestGrowthSave()
    {
        nestGrowth = nestGrowth + 200;
    }

    public void NestGrowthReset()
    {
        nestGrowth = 0;
    }
}
