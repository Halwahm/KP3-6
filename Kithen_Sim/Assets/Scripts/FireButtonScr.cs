using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonScr : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private ParticleSystem smokeParticleSystem;
    internal float temperature = 0f;
    private bool isBurning = false;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Material newMaterial;
    private Material originalMaterial;

    [SerializeField] private KrishkaScript krishkaScript;
    [SerializeField] private GameObject KrishkaGameObject;
    [SerializeField] private Material KrishkaMaterial;
    private Material originalKrishkaMaterial;

    private void Start()
    {
        fireParticleSystem.Stop();
        smokeParticleSystem.Stop();
    }

    public void ToggleFire()
    {
        Invoke(nameof(TurnOnFire), 0.5f);
        GetComponent<Button>().interactable = false;
        isBurning = true;
        StartCoroutine(UpdateTemperature());
        timerText.text = "“еперь откройте крышку и вскип€тите воду";
        SetMaterial();
    }

    private void TurnOnFire()
    {
        fireParticleSystem.Play();
        smokeParticleSystem.Play();
        fireSound.Play();
    }

    internal IEnumerator UpdateTemperature()
    {
        while (isBurning)
        {
            temperature = Random.Range(89f, 93f);
            yield return new WaitForSeconds(3f);
        }
    }

    internal void SetMaterial()
    {
        Renderer renderer = targetGameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (originalMaterial == null)
                originalMaterial = renderer.material;
            renderer.material = newMaterial;
        }
        Renderer rendererKrish = KrishkaGameObject.GetComponent<Renderer>();
        if(rendererKrish != null)
        {
            if (originalMaterial == null)
                originalMaterial = rendererKrish.material;
            rendererKrish.material = newMaterial;
        }
    }

    internal void ResetMaterial()
    {
        if (targetGameObject != null && originalMaterial != null && KrishkaGameObject != null)
        {
            Renderer renderer = targetGameObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material = originalMaterial;
            Renderer rendererKrish = KrishkaGameObject.GetComponent<Renderer>();
            if (rendererKrish != null)
                rendererKrish.material = originalMaterial;
        }
    }
}
