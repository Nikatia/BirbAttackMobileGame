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
    public GameObject kite;
    public GameObject net;
    public GameObject spawn;
    public GameObject craftMainButton;

    private int randomNumber, sticks, strings;
    private int maxSpeederSticks, maxKiteSticks, maxNetSticks;
    private int maxSpeederStrings, maxKiteStrings, maxNetStrings;
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

        maxSpeederSticks = 2;
        maxSpeederStrings = 4;

        maxKiteSticks = 4;
        maxKiteStrings = 8;

        maxNetSticks = 6;
        maxNetStrings = 30;

        BoughtOrNot();
    }

    // Update is called once per frame
    void Update()
    {
        CraftTextNumbersAndButtons();
    }

    public void ItemRoulette(float deathPositionX, float deathPositionY, float deathPositionZ)
    {
        //place where bird died
        instPosition = new Vector3(deathPositionX, deathPositionY, deathPositionZ);

        randomNumber = Random.Range(0, 100);

        if (randomNumber <=100 && randomNumber > 65)
        {
            //Stick is aquired
            noDestroyData.GetComponent<NoDestroyData>().AddStick();
            sticks = noDestroyData.GetComponent<NoDestroyData>().sticks;
            Instantiate(stickPrefab, instPosition, Quaternion.Euler(stickRotation));
            StartCoroutine(PlusOn());
        }
        else if (randomNumber <= 65 && randomNumber > 30)
        {
            //String is aquired
            noDestroyData.GetComponent<NoDestroyData>().AddString();
            strings = noDestroyData.GetComponent<NoDestroyData>().strings;
            Instantiate(stringPrefab, instPosition, Quaternion.Euler(stringRotation));
            StartCoroutine(PlusOn());
        }
        else
        {
            //RNG GOD says NOPE
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
        sticks = sticks - maxSpeederSticks;
        strings = strings - maxSpeederStrings;
        noDestroyData.GetComponent<NoDestroyData>().sticks = sticks;
        noDestroyData.GetComponent<NoDestroyData>().strings = strings;
        //Debug.Log("Speeder bought");
    }

    public void TurnOnKite()
    {
        noDestroyData.GetComponent<NoDestroyData>().TurnOnKite();
        kiteBought = true;
        kite.SetActive(true);
        craftKiteButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        sticks = sticks - maxKiteSticks;
        strings = strings - maxKiteStrings;
        noDestroyData.GetComponent<NoDestroyData>().sticks = sticks;
        noDestroyData.GetComponent<NoDestroyData>().strings = strings;
        //Debug.Log("Kite bought");
    }

    public void TurnOnNet()
    {
        noDestroyData.GetComponent<NoDestroyData>().TurnOnNet();
        netBought = true;
        craftNetButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        net.SetActive(true);
        sticks = sticks - maxNetSticks;
        strings = strings - maxNetStrings;
        //Debug.Log("Net bought");

        //moving to next level is not needed anymore, as house is protected with net
        spawn.GetComponent<Win>().enabled = false;
    }

    //changes the crafting texts according to owned items and enables button if player has enough of items
    private void CraftTextNumbersAndButtons()
    {
        //Speeder
        slingSpeederSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/" + maxSpeederSticks;
        slingSpeederStrings.GetComponent<TextMeshProUGUI>().text = strings + "/" + maxSpeederStrings;
        if (sticks >= maxSpeederSticks && strings >= maxSpeederStrings && speederBought == false)
        {
            craftSpeederButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftSpeederButton.GetComponent<Button>().interactable = false;
        }

        //Kite
        kiteSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/" + maxKiteSticks;
        kiteStrings.GetComponent<TextMeshProUGUI>().text = strings + "/" + maxKiteStrings;
        if (sticks >= maxKiteSticks && strings >= maxKiteStrings && kiteBought == false)
        {
            craftKiteButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftKiteButton.GetComponent<Button>().interactable = false; ;
        }

        //Net
        netSticks.GetComponent<TextMeshProUGUI>().text = sticks + "/" + maxNetSticks;
        netStrings.GetComponent<TextMeshProUGUI>().text = strings + "/" + maxNetStrings;
        if (sticks >= maxNetSticks && strings >= maxNetStrings && netBought == false)
        {
            craftNetButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftNetButton.GetComponent<Button>().interactable = false; ;
        }

        //Craft main button changes depending if there is anything available for crafting
        if ((sticks >= maxSpeederSticks && strings >= maxSpeederStrings && speederBought == false) || 
            (sticks >= maxKiteSticks && strings >= maxKiteStrings && kiteBought == false) ||
            (sticks >= maxNetSticks && strings >= maxNetStrings && netBought == false))
        {
            craftMainButton.GetComponent<RawImage>().color = new Color32(190, 167, 150, 255);
        }
        else
        {
            craftMainButton.GetComponent<RawImage>().color = Color.white;
        }
    }

    private void BoughtOrNot()
    {
        //checks in saved data if speeder was bought 
        speederBought = noDestroyData.GetComponent<NoDestroyData>().speederOn;
        if (speederBought == true)
        {
            craftSpeederButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        }

        //checks in saved data if kite was bought
        kiteBought = noDestroyData.GetComponent<NoDestroyData>().kiteOn;
        if (kiteBought == true)
        {
            kite.SetActive(true);
            craftKiteButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        }
    }
}
