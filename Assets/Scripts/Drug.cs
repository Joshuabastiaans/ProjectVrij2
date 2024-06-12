using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class Drug : MonoBehaviour
{
    [SerializeField] bool loadNextScene;
    bool taken;

    [SerializeField] Animator drugTransition;
    [SerializeField] Animator tripCam;
    private AmbienceMusicChange ambienceMusicChange;
    private FMOD.ATTRIBUTES_3D attributes;
    private EventInstance suckSoundInstance;
    public bool darkWave;
    Animator animator;
    PlayerAudio playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ambienceMusicChange = GetComponent<AmbienceMusicChange>();
        attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        suckSoundInstance = AudioManager.instance.CreateEventInstance(FMODEvents.instance.suckSound);
        suckSoundInstance.set3DAttributes(attributes);
        playerAudio = FindObjectOfType<PlayerAudio>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDrug()
    {
        if (taken) return;
        suckSoundInstance.start();
        ambienceMusicChange.ParameterChange();
        taken = true;
        if (darkWave)
        {
            playerAudio.PlayDarkWave();
        }

        if (loadNextScene)
        {
            Invoke("LoadNextScene", 8);
            animator.SetTrigger("dissapear");
            drugTransition.SetTrigger("leave");
        }
        else
        {
            animator.SetTrigger("dissapear");
            drugTransition.SetTrigger("stay");
            tripCam.SetTrigger("trip");
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
