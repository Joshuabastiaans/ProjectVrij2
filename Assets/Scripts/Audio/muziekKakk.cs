using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class muziekKakk : MonoBehaviour
{

    private EventInstance releaseMusic;
    private FMOD.ATTRIBUTES_3D attributes;

    // Start is called before the first frame update
    void Start()
    {
        attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        releaseMusic = AudioManager.instance.CreateEventInstance(FMODEvents.instance.releaseMusic);
        releaseMusic.set3DAttributes(attributes);
        releaseMusic.start();
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        releaseMusic.set3DAttributes(attributes);
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        releaseMusic.setParameterByName("ReleaseMusicVolume", 1f);

    }
}
