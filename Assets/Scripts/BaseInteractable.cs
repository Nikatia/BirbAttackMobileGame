using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    public UnityEvent onEnterEvents;
    public UnityEvent onExitEvents;
    public void OnEnterInteract()
    {
        onEnterEvents.Invoke();
    }

    public void OnExitInteract()
    {
        onEnterEvents.Invoke();
    }
}
