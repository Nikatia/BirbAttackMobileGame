using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject birbPrefab;
    private int rounds = 0;
    public int maxRounds;
    public float spawnTime;
    public bool done;
    public GameObject rain;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAndSpawn());
        StartCoroutine(StartRain());
        done = false;
    }

    IEnumerator MoveAndSpawn()
    {
        if (rounds <= maxRounds) {
            rounds++;
            var position = new Vector3(-900f, Random.Range(90f, 202f), Random.Range(-27f, 135f));
            Instantiate(birbPrefab, position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            yield return new WaitForSeconds(spawnTime);
            StartCoroutine(MoveAndSpawn());
        }
        else
        {
            done = true;
        }
    }

    IEnumerator StartRain()
    {
        yield return new WaitForSeconds(36f);
        rain.SetActive(true);
    }
}
