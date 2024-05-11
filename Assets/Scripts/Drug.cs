using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drug : MonoBehaviour
{
    [SerializeField] string sceneName;
    bool taken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDrug()
    {
        taken = true;
        FindObjectOfType<HUDCanvas>().PlayDrugTransition();
        Invoke("LoadScene", 8);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
