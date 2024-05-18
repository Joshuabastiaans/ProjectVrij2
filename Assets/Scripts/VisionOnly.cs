using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionOnly : MonoBehaviour
{
    [SerializeField] GameObject visibleObject;
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
            visibleObject.SetActive(false);
        }
        else
        {
            visibleObject.SetActive(true);
        }
    }
}
