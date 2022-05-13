using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public string sceneToLoad = "Scene 2";

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
            if (SceneManager.GetActiveScene().name == "Scene 1" && GameManager.instance.petalCounter >= 5)
            {
                SceneManager.LoadScene(sceneToLoad);
            }

            if (SceneManager.GetActiveScene().name == "Scene 2")
            {
                SceneManager.LoadScene(sceneToLoad);
            }

            if (SceneManager.GetActiveScene().name == "Scene 3")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
