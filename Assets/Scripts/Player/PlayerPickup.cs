using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] Transform objectGrabPointTransform;
    [SerializeField] float pickupDistance = 2;
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject reticle;

    GrabbableObject grabbableObject;
    Drug drug;

    private PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => Interact();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hit, pickupDistance, groundLayer))
        {
            objectGrabPointTransform.position = hit.point;
        }


        ShowReticle();
    }
    void Interact()
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
