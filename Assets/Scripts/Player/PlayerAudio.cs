using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;

public class PlayerAudio : MonoBehaviour
{
    public bool isGrounded;

    // sound related variables
    private Vector3 previousPosition;
    private float velocity;
    [SerializeField] private float velocitySoundThreshold = 0.2f;
    private EventInstance playerFootsteps;
    private EventInstance caveAmbience;
    private FMOD.ATTRIBUTES_3D attributes;
    private Transform footstepsReferenceLocation;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAudio();

    }

    void InitializeAudio()
    {
        // initialize footsteps
        footstepsReferenceLocation = transform.Find("GroundCheck");
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        attributes = FMODUnity.RuntimeUtils.To3DAttributes(footstepsReferenceLocation);
        playerFootsteps.set3DAttributes(attributes);

        // initialize cave ambience
        caveAmbience = AudioManager.instance.CreateEventInstance(FMODEvents.instance.caveAmbience);


    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }

    public void UpdateSound()
    {
        if (isGrounded)
        {
            // if moving play footsteps
            Vector3 currentPosition = transform.position;
            if (previousPosition != null)
            {
                float distanceTravelled = Vector3.Distance(previousPosition, currentPosition);
                float timeTaken = Time.deltaTime;
                velocity = distanceTravelled / timeTaken;
            }
            previousPosition = currentPosition;


            attributes = FMODUnity.RuntimeUtils.To3DAttributes(footstepsReferenceLocation);
            playerFootsteps.set3DAttributes(attributes);

            if (velocity > velocitySoundThreshold)
            {
                PLAYBACK_STATE playbackState;
                playerFootsteps.getPlaybackState(out playbackState);

                if (playbackState != PLAYBACK_STATE.PLAYING)
                {
                    playerFootsteps.start();
                }
            }
            else
            {
                playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }

    public void LandingSound()
    {

    }

    private void OnDestroy()
    {
        playerFootsteps.release();
    }
}
