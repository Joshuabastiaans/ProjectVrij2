using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtrudingBrick : MonoBehaviour
{
    [SerializeField] bool masterBrick;
    [SerializeField] float deactivationTime = 0.5f;

    public List<ProtrudingBrick> neighbours;

    public bool hovering;
    public bool canProtrude;
    public bool neighbourActive;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        neighbours = new List<ProtrudingBrick>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (masterBrick)
        {
            canProtrude = true;
        }

        if (hovering && canProtrude)
        {
            animator.SetBool("hovering", true);
        }
        else
        {
            animator.SetBool("hovering", false);
        }
    }

    private void OnMouseEnter()
    {
        hovering = true;
        StopAllCoroutines();

        foreach (ProtrudingBrick neighbour in neighbours)
        {
            neighbour.EnableProtrude();
        }
    }

    private void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(DelayedDeactivation());

        foreach (ProtrudingBrick neighbour in neighbours)
        {
            neighbour.DisableProtrude();
        }
    }

    IEnumerator DelayedDeactivation()
    {
        yield return new WaitForSeconds(deactivationTime);
        hovering = false;
    }

    public void EnableProtrude()
    {
        canProtrude = true;
        Debug.Log("Protrution Enabled");
    }

    public void DisableProtrude()
    {
        canProtrude = false;
        Debug.Log("Protrution Disabled");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<ProtrudingBrick>())
        {
            neighbours.Add(collision.gameObject.GetComponent<ProtrudingBrick>());
        }
    }
}
