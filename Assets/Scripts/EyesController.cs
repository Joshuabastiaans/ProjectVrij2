using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesController : MonoBehaviour
{
    public bool eyesClosed;
    [SerializeField] KeyCode eyesInput = KeyCode.Mouse1;

    [SerializeField] Animator eyeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(eyesInput))
        {
            CloseEyes();
        }

        if (!Input.GetKey(eyesInput) && eyesClosed)
        {
            OpenEyes();
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
