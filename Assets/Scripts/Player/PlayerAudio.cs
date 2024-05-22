using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] walkingSounds;
    [SerializeField] AudioSource walkingSoundsSource;
    [SerializeField] float walkingSoundIntervalTime = 0.5f;
    [Space]
    [SerializeField] AudioClip landingSound;
    public bool IsWalking;

    int stepCount = 0;
    bool isPlayingSteps = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WalkingSounds();
    }

    void WalkingSounds()
    {
        if (IsWalking)
        {
            if (!isPlayingSteps)
            {
                isPlayingSteps = true;
                StartCoroutine(PlaySteps());
            }
        }
        else
        {
            StopAllCoroutines();
            isPlayingSteps = false;
        }
    }
    IEnumerator PlaySteps()
    {
        walkingSoundsSource.PlayOneShot(walkingSounds[stepCount]);
        yield return new WaitForSeconds(walkingSoundIntervalTime);
        stepCount++;
        if (stepCount >= walkingSounds.Length)
        {
            stepCount = 0;
        }
        StartCoroutine(PlaySteps());
    }

    public void LandingSound()
    {

    }
}
