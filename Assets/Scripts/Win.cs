using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject nest;
    public GameObject spawn;
    public GameObject analytics;
    public GameObject winlUI;
    public GameObject sling;
    public GameObject craftButton;
    public int birbs;
    public bool win;

    private bool roundsDone;
    private bool nestDone;
    private bool areBirbs;
    private bool winAnaTriggered;
    private int maxBirbs;
    private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        areBirbs = true;
        maxBirbs = spawn.GetComponent<Spawning>().maxRounds;
        maxBirbs++;
        birbs = 0;
        win = false;
        nestDone = false;
        winAnaTriggered = false;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        roundsDone = spawn.GetComponent<Spawning>().done; //checking if spawner has done its job
        nestDone = nest.GetComponent<Nest>().nestDone; //checking if nest is not done
        if (birbs == maxBirbs) //checking if any birds exist
        {
            if (GameObject.FindWithTag("Birb") == null)
            {
                areBirbs = false;
            }
        }
        
        //if all birds have been spawned, nest is not done and there is no birds alive, then player wins
        if (roundsDone == true && areBirbs == false && nestDone == false)
        {
            winlUI.SetActive(true);
            sling.GetComponent<Shooting>().enabled = false;
            win = true;

            if (!winAnaTriggered)
            {
                WinAnalyticsTrigger();
            }

            if (sceneName == "7thChallenge")
            {
                craftButton.SetActive(false);
            }
        }
    }

    public void AddBirb()
    {
        birbs++;
    }

    private void WinAnalyticsTrigger()
    {
        winAnaTriggered = true;

        //launches analytics script: level succeeded method.
        analytics.GetComponent<Analytics>().LevelSucceeded();
    }
}
