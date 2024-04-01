using UnityEngine;
using UnityEngine.UI;

public class PearlBtnScr : MonoBehaviour
{
    [SerializeField] private Button[] nextButton;
    [SerializeField] private Button[] otherButtons;

    void Start()
    {
        DeactivateOtherButtons();
    }

    public void Toggle()
    {
        ActivateOtherButtons();
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
        foreach (Button button in nextButton)
        {
            button.interactable = true;
        }
    }
}
