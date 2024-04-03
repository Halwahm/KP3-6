using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonScr : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Animator drovAnim;

    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private ParticleSystem smokeParticleSystem;

    [SerializeField] private Button[] otherButtons;
    [SerializeField] private Button nextButton;
    internal float temperature;

    private void Start()
    {
        DeactivateOtherButtons();
        fireParticleSystem.Stop();
        smokeParticleSystem.Stop();
        doorAnim.SetBool("doorAnim", false);
        drovAnim.SetBool("drovAnim", false);
    }

    public void PlayAnims()
    {
        doorAnim.SetBool("doorAnim", true);
        drovAnim.SetBool("drovAnim", true);
    }

    public void ToggleFire()
    {
        Invoke("TurnOnFire", 3f);
        GetComponent<Button>().interactable = false;
        StartCoroutine(UpdateTemperature());
        StartCoroutine(ActivateOtherButtons());
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

    private IEnumerator ActivateOtherButtons()
    {
        yield return new WaitForSeconds(3f);
        nextButton.interactable = true;
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
