using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private bool menuPressed;

    public GameObject menu;

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
}
