using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Hand.instance.isGrabbing)
            {
                gameObject.SetActive(true);
            }
            if (!Hand.instance.isGrabbing)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Hand.instance.isGrabbing)
            {
                gameObject.SetActive(false);
            }
            if (!Hand.instance.isGrabbing)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
