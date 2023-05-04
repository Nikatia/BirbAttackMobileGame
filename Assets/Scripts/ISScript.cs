using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISScript : MonoBehaviour
{
    private string appKey = "19d2d39cd";
    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.init(appKey);

    }

    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void SdkInitializationCompletedEvent() 
    {
        IronSource.Agent.validateIntegration();
    }

    //------====CALLBACKS====------
    //Banner

    //FullSize

    //Rewarded
}
