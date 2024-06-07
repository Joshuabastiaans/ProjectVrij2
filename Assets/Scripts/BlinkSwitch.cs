using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkSwitch : MonoBehaviour
{
    [SerializeField] GameObject versionA;
    [SerializeField] GameObject versionB;
    bool switched;
    bool eyesClosed;

    EyesController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<EyesController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.eyesClosed)
        {
            if (eyesClosed) { return; }
            eyesClosed = true;
            switched = !switched;
            versionA.SetActive(!switched);
            versionB.SetActive(switched);
        }
        else
        {
            eyesClosed = false;
        }
    }
}
