using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private bool menuPressed;

    public GameObject menu;

    [SerializeField] private Material normalPetal;
    [SerializeField] private Material transparentPetal;
    [Space]
    [SerializeField] private Renderer petal1;
    [SerializeField] private Renderer petal2;
    [SerializeField] private Renderer petal3;
    [SerializeField] private Renderer petal4;
    [SerializeField] private Renderer petal5;

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

        if (leftHandDevices.Count > 0)
        {
            // If menu button is pressed on left hand device
            if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuPressed) && menuPressed)
            {
                // Open menu
                Debug.Log("Menu button is pressed.");
                menu.SetActive(true);
            }
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
                petal1.material = normalPetal;
                petal2.material = normalPetal;
                petal3.material = normalPetal;
                petal4.material = normalPetal;
                petal5.material = normalPetal;
                break;

            case 4:
                petal1.material = normalPetal;
                petal2.material = normalPetal;
                petal3.material = normalPetal;
                petal4.material = normalPetal;
                petal5.material = transparentPetal;
                break;

            case 3:
                petal1.material = normalPetal;
                petal2.material = normalPetal;
                petal3.material = normalPetal;
                petal4.material = transparentPetal;
                petal5.material = transparentPetal;
                break;

            case 2:
                petal1.material = normalPetal;
                petal2.material = normalPetal;
                petal3.material = transparentPetal;
                petal4.material = transparentPetal;
                petal5.material = transparentPetal;
                break;

            case 1:
                petal1.material = normalPetal;
                petal2.material = transparentPetal;
                petal3.material = transparentPetal;
                petal4.material = transparentPetal;
                petal5.material = transparentPetal;
                break;

            case 0:
                petal1.material = transparentPetal;
                petal2.material = transparentPetal;
                petal3.material = transparentPetal;
                petal4.material = transparentPetal;
                petal5.material = transparentPetal;
                break;
        }
    }
}
