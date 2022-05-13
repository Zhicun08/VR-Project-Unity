using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    public GameObject menu;
    AudioSource _audioSource;
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Hand"))
        {
            _audioSource.PlayOneShot(sound);
            Debug.Log("Close Menu.");
            menu.SetActive(false);
        }
    }
}
