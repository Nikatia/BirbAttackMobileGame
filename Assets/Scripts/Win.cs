using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject nest;
    public GameObject spawn;
    public int birbs;
    public GameObject winlUI;
    public bool win;

    private bool roundsDone;
    private bool nestDone;
    private bool areBirbs;
    private int maxBirbs;
    // Start is called before the first frame update
    void Start()
    {
        areBirbs = true;
        maxBirbs = spawn.GetComponent<Spawning>().maxRounds;
        maxBirbs++;
        birbs = 0;
        win = false;
        nestDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        roundsDone = spawn.GetComponent<Spawning>().done;
        nestDone = nest.GetComponent<Nest>().nestDone;
        if (birbs == maxBirbs)
        {
            areBirbs = false;
        }
        if (roundsDone == true && areBirbs == false && nestDone == false)
        {
            winlUI.SetActive(true);
            win = true;
        }
    }

    public void AddBirb()
    {
        birbs++;
    }
}
