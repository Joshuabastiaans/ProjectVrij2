using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class muziekKakk1 : MonoBehaviour
{

    private EventInstance breeze;
    private FMOD.ATTRIBUTES_3D attributes;

    // Start is called before the first frame update
    void Start()
    {
        attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        breeze = AudioManager.instance.CreateEventInstance(FMODEvents.instance.breeze);
        breeze.set3DAttributes(attributes);
        breeze.start();
    }

    // Update is called once per frame
    void Update()
    {
        attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        breeze.set3DAttributes(attributes);
    }
}
