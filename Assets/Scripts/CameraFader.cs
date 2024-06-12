using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraFader : MonoBehaviour
{
    public Camera overlayCam1;
    public Camera overlayCam2;
    public Image fadeImage; // The UI Image component that covers the whole screen
    public float fadeDuration = 1.0f;

    private void Start()
    {
        overlayCam1.enabled = true;
        overlayCam2.enabled = false;
    }

    public void FadeToSecondCamera()
    {
        StartCoroutine(FadeCameras(overlayCam1, overlayCam2));
    }

    private IEnumerator FadeCameras(Camera cam1, Camera cam2)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = new Color(0, 0, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);

        // Switch cameras
        cam1.enabled = false;
        cam2.enabled = true;

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = new Color(0, 0, 0, 1 - (elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
    }
}
