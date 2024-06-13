using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference waterDroplets { get; private set; }
    [field: SerializeField] public EventReference caveAmbience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference spookyMusic { get; private set; }
    [field: SerializeField] public EventReference amenMusic { get; private set; }
    [field: SerializeField] public EventReference releaseMusic { get; private set; }
    [field: SerializeField] public EventReference breeze { get; private set; }


    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerBreathing { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference suckSound { get; private set; }
    [field: SerializeField] public EventReference darkWave { get; private set; }


    public static FMODEvents instance { get; private set; }
    private Dictionary<string, string> events;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;

        events = new Dictionary<string, string>
        {
            { "playerFootsteps", "event:/Footsteps" },
            { "playerBreathing", "event:/Breathing" },
            { "caveAmbience", "event:/CaveAmbience" },
            { "waterDroplets", "event:/WaterDroplets" },
            { "spookyMusic", "event:/SpookyMusic"},
        };
    }
}