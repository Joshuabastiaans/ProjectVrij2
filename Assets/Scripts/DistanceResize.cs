using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceResize : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float strength = 1;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        float resizing = distance * strength;
        transform.localScale = new Vector3(resizing, resizing, resizing);
    }
}
