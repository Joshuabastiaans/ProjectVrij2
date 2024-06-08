using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LookAtPoint : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 100f;
    public float gazeTime = 2f;
    private float timer;
    private RaycastHit hitInfo;
    private GameObject lastLookedObject = null;
    private Material originalMaterial;
    public Volume volume;
    private PsychedelicEffect psychedelicEffect;

    void Start()
    {
        volume.profile.TryGet(out psychedelicEffect);
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, maxDistance))
        {
            if (hitInfo.collider != null)
            {
                GameObject lookedObject = hitInfo.collider.gameObject;

                if (lookedObject != lastLookedObject)
                {
                    ResetLastLookedObject();
                    lastLookedObject = lookedObject;
                    originalMaterial = lookedObject.GetComponent<Renderer>().material;
                }

                timer += Time.deltaTime;
                if (timer >= gazeTime)
                {
                    TriggerPsychedelicEffect();
                    timer = 0;
                }
            }
        }
        else
        {
            ResetLastLookedObject();
            timer = 0;
        }
    }

    void TriggerPsychedelicEffect()
    {
        psychedelicEffect.intensity.value = 1.0f;
    }

    void ResetLastLookedObject()
    {
        if (lastLookedObject != null)
        {
            psychedelicEffect.intensity.value = 0.0f;
            lastLookedObject = null;
        }
    }
}
