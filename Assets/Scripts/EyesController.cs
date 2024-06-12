using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesController : MonoBehaviour
{
    public bool eyesClosed;
    [SerializeField] Animator eyeAnimator;
    private PlayerControls controls;

    bool canceling;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Blink.performed += ctx => CloseEyes();
        controls.Player.Blink.canceled += ctx =>
        {
            canceling = true;
        };

    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        // CloseEyes();


        // if (eyesClosed)
        // {
        //     OpenEyes();
        // }

        if (canceling)
        {
            canceling = true;

            if (eyesClosed)
            {
                OpenEyes();
                canceling = false;
            }
        }
    }

    IEnumerator OpeningEyes()
    {
        if (!eyesClosed)
        {
            yield return null;
        }
        OpenEyes();
    }

    void CloseEyes()
    {
        eyeAnimator.SetBool("close", true);
    }

    void OpenEyes()
    {
        eyeAnimator.SetBool("close", false);
    }

    void EyesClosed()
    {
        eyesClosed = true;
        Debug.Log("Eyes Closed");
    }

    void EyesOpen()
    {
        eyesClosed = false;
    }
}
