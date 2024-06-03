using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSightline : MonoBehaviour
{
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<ProtrudingBrick>())
            {
                hit.collider.gameObject.GetComponent<ProtrudingBrick>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
