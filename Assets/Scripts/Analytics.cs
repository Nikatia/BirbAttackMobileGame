using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Analytics : MonoBehaviour
{
    private string sceneName;
    private GameObject[] birbs;
    private int shots, birbsKilled, timeInSeconds, leftBirbsCount;
    private float levelTimer;
    private bool updateTimer = false, nestCleared;

    // Start is called before the first frame update
    async void Start()
    {
        updateTimer = true;
        levelTimer = 0.0f;
        timeInSeconds = 0;
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        shots = 0;
        birbsKilled = 0;
        nestCleared = false;

        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.Reason);
            Debug.Log("WTF");
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }
        Debug.Log("Went ok?");
    }

    // Update is called once per frame
    void Update()
    {
        //level timer
        if (updateTimer)
        {
            levelTimer += Time.deltaTime * 1;
            timeInSeconds = Mathf.RoundToInt(levelTimer);
        }
    }

    public void BirbKilled(float birbSpeed)
    {
        birbsKilled++;
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "sceneName", sceneName },
            { "birbSpeed", birbSpeed }
        };
        AnalyticsService.Instance.CustomData("birbKilled", parameters);
    }

    public void BirbReachedHouse(float birbSpeed)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "sceneName", sceneName },
            { "birbSpeed", birbSpeed }
        };
        AnalyticsService.Instance.CustomData("birbReachedHouse", parameters);
    }

    public void LevelFailed()
    {
        updateTimer = false;

        birbs = GameObject.FindGameObjectsWithTag("Birb");
        leftBirbsCount = 0;
        if (birbs != null)
        {
            foreach (GameObject birb in birbs)
            {
                leftBirbsCount++;
            }
        }

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "sceneName", sceneName },
            { "birbsLeft", leftBirbsCount },
            { "playTime", timeInSeconds }
        };
        AnalyticsService.Instance.CustomData("levelFailed", parameters);
    }

    public void AddShots()
    {
        shots++;
    }

    public void LevelSucceeded()
    {
        updateTimer = false;

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "sceneName", sceneName },
            { "shots", shots },
            { "birbsKilled", birbsKilled },
            { "playTime", timeInSeconds }
        };
        AnalyticsService.Instance.CustomData("levelSucceeded", parameters);
    }

    public void ChoseToClearNest()
    {
        nestCleared = true;

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "nestClearing", true }
        };
        AnalyticsService.Instance.CustomData("clearNest", parameters);
    }

    public void ChoseToNotClearNest()
    {
        if (!nestCleared)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "nestClearing", false }
        };
            AnalyticsService.Instance.CustomData("clearNest", parameters);
        }
    }

    public void Replay()
    {
        AnalyticsService.Instance.CustomData("replay");
    }

}
