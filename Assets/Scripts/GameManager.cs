using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int petalCounter;


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
        
    }

    public void UpdateProgress()
    {
        petalCounter++;
        Debug.Log("Collected " + petalCounter + " petal.");
        // Display to UI

    }
}
