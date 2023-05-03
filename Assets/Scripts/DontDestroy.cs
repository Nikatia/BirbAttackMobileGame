using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public GameObject spawn;
    Win win;
    private bool inScene;


    void Start()
    {
        win = spawn.GetComponent<Win>();        
        inScene = true;
    }

    void Update()
    {
        if (spawn == null)
        {
            spawn = GameObject.Find("Spawn");
            win = spawn.GetComponent<Win>();
        }

        if (win.win == true)
        {
            DontDestroyOnLoad(transform.gameObject);
            inScene = false;
        }
        else
        {
            inScene = true;
        }
    }

    public void MoveToScene() 
    {
        SceneManager.MoveGameObjectToScene(transform.gameObject, SceneManager.GetActiveScene());
    }
}


