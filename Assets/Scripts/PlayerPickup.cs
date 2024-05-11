using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] Transform objectGrabPointTransform;
    [SerializeField] float pickupDistance = 2;
    [SerializeField] KeyCode pickUpInput = KeyCode.Mouse0;
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject reticle;

    GrabbableObject grabbableObject;
    Drug drug;

    void Update()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hit, pickupDistance, groundLayer))
        {
            objectGrabPointTransform.position = hit.point;
        }

        if (Input.GetKeyDown(pickUpInput))
        {
            if (grabbableObject == null)
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayer))
                {
                    if (raycastHit.transform.TryGetComponent(out grabbableObject))
                    {
                        Debug.Log(grabbableObject);
                        grabbableObject.Grab(objectGrabPointTransform);
                    }
                    else if (raycastHit.transform.TryGetComponent(out drug))
                    {
                        drug.TakeDrug();
                    }
                }
            }
            else
            {
                grabbableObject.Drop();
                grabbableObject = null;
            }
        }

        ShowReticle();
    }

    void ShowReticle()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayer)
            && (grabbableObject == null))
        {
            reticle.SetActive(true);
        }
        else
        {
            reticle.SetActive(false);
        }
    }

}
