using UnityEngine;
using UnityEngine.UI;

public class FireButtonScr : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Animator drovAnim;

    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private AudioSource fireSound;

    [SerializeField] private Button[] otherButtons;
    [SerializeField] private Button nextButton;

    private void Start()
    {
        DeactivateOtherButtons();
        fireParticleSystem.Stop();
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
        ActivateOtherButtons();
        Invoke("TurnOnFire", 4f);
        GetComponent<Button>().interactable = false;
    }

    private void TurnOnFire()
    {
        fireParticleSystem.Play();
        fireSound.Play();
    }

    private void DeactivateOtherButtons()
    {
        foreach (Button button in otherButtons)
        {
            button.interactable = false;
        }
    }

    private void ActivateOtherButtons()
    {
         nextButton.interactable = true;
    }
}
