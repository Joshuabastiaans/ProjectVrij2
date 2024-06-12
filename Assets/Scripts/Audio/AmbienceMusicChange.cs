using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceMusicChange : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;
    private PlayerAudio playerAudio;

    private void Start()
    {
        playerAudio = FindObjectOfType<PlayerAudio>();
    }

    public void ParameterChange()
    {
        playerAudio.SetAmbienceParameter(parameterName, parameterValue);
    }
}
