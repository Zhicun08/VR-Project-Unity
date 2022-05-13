using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int petalCounter;

    public GameObject waterfall;
    public GameObject portal;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        petalCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UIController.instance.ProgressDisplay();
        if (petalCounter >= 5)
        {
            waterfall.SetActive(false);
            portal.SetActive(true);
        }
    }

    public void UpdateProgress()
    {
        petalCounter++;
        Debug.Log("Collected " + petalCounter + " petal.");
        // Display to UI
        UIController.instance.ProgressDisplay();
    }
}
