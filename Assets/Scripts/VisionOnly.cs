using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionOnly : MonoBehaviour
{
    [SerializeField] GameObject visibleObject;
    EyesController controller;

    Camera cam;
    Plane[] cameraFrostum;
    Collider coll;

    bool inObject;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<EyesController>();

        cam = Camera.main;
        coll = GetComponent<Collider>();
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
            var bounds = coll.bounds;
            cameraFrostum = GeometryUtility.CalculateFrustumPlanes(cam);
            if (GeometryUtility.TestPlanesAABB(cameraFrostum, bounds) && !inObject)
            {
                visibleObject.SetActive(true);
            }
            else
            {
                visibleObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inObject = false;
    }
}
