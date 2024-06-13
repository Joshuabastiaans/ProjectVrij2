using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;
    private PlayerAudio playerAudio;

    private bool isTriggered = false;

    private void Start()
    {
        playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            playerAudio.SetAmbienceParameter(parameterName, parameterValue);
        }
    }
}
