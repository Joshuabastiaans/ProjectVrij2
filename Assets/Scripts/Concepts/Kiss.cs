using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiss : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] AudioClip kissAudioClip;
    AudioSource audio;

    public bool isLookingAt;
    float time;
    bool kissed;

    Camera cam;
    Plane[] cameraFrostum;
    Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        coll = GetComponent<Collider>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var bounds = coll.bounds;
        cameraFrostum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(cameraFrostum, bounds))
        {
            isLookingAt = true;
        }
        else
        {
            isLookingAt = false;
        }
    }

    void CompleteKiss()
    {
        audio.PlayOneShot(kissAudioClip);
        doorAnimator.SetTrigger("open");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLookingAt)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
            }

            if (time >= 0.4f)
            {
                if (!kissed)
                {
                    CompleteKiss();
                }
                kissed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            time = 0;
        }
    }
}
