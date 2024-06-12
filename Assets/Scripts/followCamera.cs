using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{

    public Transform target;

    public void Update()
    {
        transform.rotation = target.rotation;
    }
}
