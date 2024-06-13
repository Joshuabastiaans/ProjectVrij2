using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;
using System.Collections;

public class PlayerAudio : MonoBehaviour
{
    public bool isGrounded;

    // sound related variables
    private Vector3 previousPosition;
    private float velocity;
    [SerializeField] private float velocitySoundThreshold = 0.2f;
    private EventInstance playerFootsteps;
    private EventInstance caveAmbience;
    private EventInstance playerBreathing;
    private EventInstance darkWave;
    private EventInstance amenMusic;
    private EventInstance releaseMusic;
    private FMOD.ATTRIBUTES_3D footstepAttributes;
    private FMOD.ATTRIBUTES_3D attributes;
    private Transform footstepsReferenceLocation;
    private PlayerGroundMovement playerGroundMovement;

    private EventInstance music;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAudio();
        playerGroundMovement = GetComponent<PlayerGroundMovement>();
    }

    void InitializeAudio()
    {
        // initialize footsteps
        footstepsReferenceLocation = transform.Find("GroundCheck");
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        footstepAttributes = FMODUnity.RuntimeUtils.To3DAttributes(footstepsReferenceLocation);
        playerFootsteps.set3DAttributes(footstepAttributes);

        attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);

        // initialize cave ambience
        caveAmbience = AudioManager.instance.CreateEventInstance(FMODEvents.instance.caveAmbience);
        caveAmbience.start();
        caveAmbience.set3DAttributes(attributes);

        // initialize player breathing
        playerBreathing = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerBreathing);
        playerBreathing.start();
        playerBreathing.set3DAttributes(attributes);

        // initialize music
        music = AudioManager.instance.CreateEventInstance(FMODEvents.instance.spookyMusic);
        music.start();
        music.set3DAttributes(attributes);

        // initialize dark wave
        darkWave = AudioManager.instance.CreateEventInstance(FMODEvents.instance.darkWave);
        darkWave.set3DAttributes(attributes);

        // initialize amen music
        amenMusic = AudioManager.instance.CreateEventInstance(FMODEvents.instance.amenMusic);
        amenMusic.set3DAttributes(attributes);
        amenMusic.start();

        // initialize release music
        releaseMusic = AudioManager.instance.CreateEventInstance(FMODEvents.instance.releaseMusic);
        releaseMusic.set3DAttributes(attributes);
        releaseMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }

    public void UpdateSound()
    {
        // if moving play footsteps
        Vector3 currentPosition = transform.position;
        if (previousPosition != null)
        {
            float distanceTravelled = Vector3.Distance(previousPosition, currentPosition);
            float timeTaken = Time.deltaTime;
            velocity = distanceTravelled / timeTaken;

            footstepAttributes = FMODUnity.RuntimeUtils.To3DAttributes(footstepsReferenceLocation);
            playerFootsteps.set3DAttributes(footstepAttributes);
            attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
            caveAmbience.set3DAttributes(attributes);
            playerBreathing.set3DAttributes(attributes);
            music.set3DAttributes(attributes);
            darkWave.set3DAttributes(attributes);
            amenMusic.set3DAttributes(attributes);
            releaseMusic.set3DAttributes(attributes);
        }
        previousPosition = currentPosition;

        if (velocity > velocitySoundThreshold && playerGroundMovement.IsGrounded())
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState != PLAYBACK_STATE.PLAYING)
            {
                print("playing footsteps");
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        music.setParameterByName(parameterName, parameterValue);
    }

    public void SetAmenMusicParameter(string parameterName, float parameterValue)
    {
        amenMusic.setParameterByName(parameterName, parameterValue);
    }

    public void SetReleaseMusicParameter(string parameterName, float parameterValue)
    {
        releaseMusic.setParameterByName(parameterName, parameterValue);
    }

    private void OnDestroy()
    {
        playerFootsteps.release();
        caveAmbience.release();
    }

    public void PlayDarkWave()
    {
        StartCoroutine(playSoundCoroutine(darkWave, 3, 5f));
    }

    IEnumerator playSoundCoroutine(EventInstance soundInstance, int amount, float timeInBetween)
    {
        for (int i = 0; i < amount; i++)
        {
            soundInstance.start();
            yield return new WaitForSeconds(timeInBetween);
        }
    }
}
