using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject birbPrefab;
    private int rounds = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAndSpawn());
    }

    IEnumerator MoveAndSpawn()
    {
        if (rounds <= 10) {
            rounds++;
            var position = new Vector3(-900f, Random.Range(90f, 202f), Random.Range(-27f, 135f));
            Instantiate(birbPrefab, position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            yield return new WaitForSeconds(5f);
            StartCoroutine(MoveAndSpawn());
        }
    }
}
