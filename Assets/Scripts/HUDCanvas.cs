using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCanvas : MonoBehaviour
{
    [SerializeField] GameObject drugTransition;

    // Start is called before the first frame update
    void Awake()
    {
        drugTransition.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDrugTransition()
    {
        drugTransition.SetActive(true);
    }
}
