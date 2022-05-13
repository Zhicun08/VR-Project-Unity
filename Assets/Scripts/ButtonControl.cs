using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject instructionMenu;

    public GameObject progressMenu;

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
            progressMenu.SetActive(false);
            instructionMenu.SetActive(true);
        }
    }
}
