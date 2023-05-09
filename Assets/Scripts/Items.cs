using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public GameObject stickPrefab;
    public GameObject stringPrefab;
    public GameObject plus;
    public GameObject slingSpeederSticks, kiteSticks, netSticks;
    public GameObject slingSpeederStrings, kiteStrings, netStrings;
    public GameObject noDestroyData;
    public GameObject craftSpeederButton, craftKiteButton, craftNetButton;
    public GameObject sling;

    private int randomNumber, sticks, strings;
    private Vector3 instPosition;
    private Vector3 stickRotation, stringRotation;
    private bool speederBought, kiteBought, netBought;


    // Start is called before the first frame update
    void Start()
    {
        noDestroyData = GameObject.Find("SaveData");

        stickRotation = new Vector3 (0, 0, 92.6f);
        stringRotation = new Vector3(25.1f, -27.1f, -38.48f);

        sticks = noDestroyData.GetComponent<NoDestroyData>().sticks;
        strings = noDestroyData.GetComponent <NoDestroyData>().strings;

        speederBought = false;
        kiteBought = false;
        netBought = false;
    }

    // Update is called once per frame
    void Update()
    {
        //changes the crafting texts according to owned items and enables button if player has enough of items
        slingSpeederSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/2";
        slingSpeederStrings.GetComponent<TextMeshProUGUI>().text = strings + "/2";
        if (sticks >= 2 && strings >= 2 && speederBought == false)
        {
            craftSpeederButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftSpeederButton.GetComponent<Button>().interactable = false; ;
        }

        kiteSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/2";
        kiteStrings.GetComponent<TextMeshProUGUI>().text = strings + "/15";
        if (sticks >= 2 && strings >= 15 && kiteBought == false)
        {
            craftKiteButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftKiteButton.GetComponent<Button>().interactable = false; ;
        }

        netSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/15";
        netStrings.GetComponent<TextMeshProUGUI>().text = strings + "/50";
        if (sticks >= 15 && strings >= 50 && netBought == false)
        {
            craftNetButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftNetButton.GetComponent<Button>().interactable = false; ;
        }
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

    public void TurnOnSpeeder()
    {
        noDestroyData.GetComponent<NoDestroyData>().TurnOnSpeeder();
        speederBought = true;
        sling.GetComponent<Shooting>().TurnOnSpeeder();
        craftSpeederButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        //Debug.Log("Speeder bought");
    }

    public void TurnOnKite()
    {
        noDestroyData.GetComponent<NoDestroyData>().TurnOnKite();
        kiteBought = true;
        Debug.Log("Kite bought");
    }

    public void TurnOnNet()
    {
        noDestroyData.GetComponent<NoDestroyData>().TurnOnNet();
        netBought = true;
        Debug.Log("Net bought");
    }
}
