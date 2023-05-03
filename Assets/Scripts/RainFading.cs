using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFading : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(StopRain());
    }

    IEnumerator StopRain()
    {
        yield return new WaitForSeconds(15f);
        var emission = ps.emission;
        emission.rateOverTime = 100;
        yield return new WaitForSeconds(6.5f);
        emission.rateOverTime = 0;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
