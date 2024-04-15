using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonScr : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private ParticleSystem smokeParticleSystem;

    [SerializeField] private Button[] otherButtons;
    internal float temperature;

    private void Start()
    {
        DeactivateOtherButtons();
        fireParticleSystem.Stop();
        smokeParticleSystem.Stop();
    }

    public void ToggleFire()
    {
        Invoke(nameof(TurnOnFire), 0.5f);
        GetComponent<Button>().interactable = false;
        StartCoroutine(UpdateTemperature());
    }

    private void TurnOnFire()
    {
        fireParticleSystem.Play();
        smokeParticleSystem.Play();
        fireSound.Play();
    }

    private void DeactivateOtherButtons()
    {
        foreach (Button button in otherButtons)
        {
            button.interactable = false;
        }
    }

    private IEnumerator UpdateTemperature()
    {
        while (true)
        {
            temperature = Random.Range(200f, 270f);
            yield return new WaitForSeconds(3f);
        }
    }
}
