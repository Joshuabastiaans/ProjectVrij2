using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;
    private PlayerAudio playerAudio;

    private void Start()
    {
        playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAudio.SetAmbienceParameter(parameterName, parameterValue);
            print("Ambience parameter changed to 32 " + parameterValue);
        }
    }
}
