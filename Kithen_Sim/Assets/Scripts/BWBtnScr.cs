using UnityEngine.UI;
using UnityEngine;

public class BWBtnScr : MonoBehaviour
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
