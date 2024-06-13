using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nieambien : MonoBehaviour
{
    private PlayerAudio playerAudio;
    private bool isTriggered = false;

    private void Start()
    {
        playerAudio = FindObjectOfType<PlayerAudio>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            playerAudio.SetAmenMusicParameter("AmenMusicVolume", 1f);
            playerAudio.SetAmbienceParameter("SpookyMusicVolume", 0f);

        }
    }
}
