using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drug : MonoBehaviour
{
    [SerializeField] bool loadNextScene;
    bool taken;

    [SerializeField] Animator drugTransition;
    [SerializeField] Animator tripCam;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDrug()
    {
        taken = true;
        if (loadNextScene)
        {
            Invoke("LoadNextScene", 8);
            animator.SetTrigger("dissapear");
            drugTransition.SetTrigger("leave");
        }
        else
        {
            animator.SetTrigger("dissapear");
            drugTransition.SetTrigger("stay");
            tripCam.SetTrigger("trip");
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
