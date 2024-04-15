using UnityEngine;
using UnityEngine.UI;

public class DoorClose : MonoBehaviour
{
    [SerializeField] private Animator doorClose;
    [SerializeField] private Button nextButton;

    private bool xKeyPressed = false;

    private void Awake()
    {
        DeactivateOtherButtons();
    }

    private void Start()
    {
        doorClose.SetBool("doorClose", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !xKeyPressed)
        {
            xKeyPressed = true;

            if (!nextButton.interactable)
                nextButton.interactable = true;

            doorClose.SetBool("doorClose", true);
            doorClose.SetBool("doorOpen", false);
        }
    }

    private void DeactivateOtherButtons()
    {
        nextButton.interactable = false;
    }
}
