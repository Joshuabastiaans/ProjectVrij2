using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjectLocations : MonoBehaviour
{
    public GameObject oldObject;
    public GameObject newObject;

    public void MoveObject()
    {
        oldObject.SetActive(false);

        newObject.SetActive(true);
    }
}
