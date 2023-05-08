using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject stickPrefab;
    public GameObject stringPrefab;
    public GameObject plus;
    public GameObject slingSpeederSticks, kiteSticks, netSticks;
    public GameObject slingSpeederStrings, kiteStrings, netStrings;
    public GameObject noDestroyData;

    private int randomNumber, sticks, strings;
    private Vector3 instPosition;
    private Vector3 stickRotation, stringRotation;
    

    // Start is called before the first frame update
    void Start()
    {
        noDestroyData = GameObject.Find("SaveData");
        stickRotation = new Vector3 (0, 0, 92.6f);
        stringRotation = new Vector3(25.1f, -27.1f, -38.48f);
        sticks = noDestroyData.GetComponent<NoDestroyData>().sticks;
        strings = noDestroyData.GetComponent <NoDestroyData>().strings;
    }

    // Update is called once per frame
    void Update()
    {
        //changes the crafting texts according to owned items
        slingSpeederSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/2";
        kiteSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/2";
        netSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/15";
        slingSpeederStrings.GetComponent<TextMeshProUGUI>().text = strings + "/2";
        kiteStrings.GetComponent<TextMeshProUGUI>().text = strings + "/15";
        netStrings.GetComponent<TextMeshProUGUI>().text = strings + "/50";
    }

    public void ItemRoulette(float deathPositionX, float deathPositionY, float deathPositionZ)
    {
        //place where bird died
        instPosition = new Vector3(deathPositionX, deathPositionY, deathPositionZ);

        randomNumber = Random.Range(0, 100);

        if (randomNumber <=100 && randomNumber > 65)
        {
            //Stick is aquired
            Debug.Log("Stick");
            noDestroyData.GetComponent<NoDestroyData>().AddStick();
            sticks = noDestroyData.GetComponent<NoDestroyData>().sticks;
            Instantiate(stickPrefab, instPosition, Quaternion.Euler(stickRotation));
            StartCoroutine(PlusOn());
        }
        else if (randomNumber <= 65 && randomNumber > 30)
        {
            //String is aquired
            Debug.Log("String");
            noDestroyData.GetComponent<NoDestroyData>().AddString();
            strings = noDestroyData.GetComponent<NoDestroyData>().strings;
            Instantiate(stringPrefab, instPosition, Quaternion.Euler(stringRotation));
            StartCoroutine(PlusOn());
        }
        else
        {
            //RNG GOD says NOPE
            Debug.Log("Nothing");
        }
    }

    IEnumerator PlusOn() //showes little plus next to crafting button
    {
        plus.SetActive(true);
        yield return new WaitForSeconds(2f);
        plus.SetActive(false);
    }
}
