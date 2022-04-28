using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private bool menuPressed;

    public GameObject menu;

    public GameObject petal1;
    public GameObject petal2;
    public GameObject petal3;
    public GameObject petal4;
    public GameObject petal5;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        // Get all left hand devices
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        // If menu button is pressed on left hand device
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuPressed) && menuPressed)
        {
            // Open menu
            Debug.Log("Menu button is pressed.");
            menu.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        Debug.Log("Menu button is pressed.");
        menu.SetActive(false);
    }

    public void ProgressDisplay()
    {
    
        switch (GameManager.instance.petalCounter)
        {
            case 5:                

                break;

            case 4:            

                break;

            case 3:

                break;

            case 2:

                break;

            case 1:

                break;

            case 0:

                break;

            default:

                break;
        }
    }
}
