using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestionButton : MonoBehaviour
{
    public GameObject helpWindow;

    private bool isHelpVisible = false;

    private void Start()
    {
        helpWindow.SetActive(false);
    }

    public void ToggleHelpWindow()
    {
        isHelpVisible = !isHelpVisible;
        helpWindow.SetActive(isHelpVisible);
    }

    public void CloseHelpWindow()
    {
        helpWindow.SetActive(false);
    }
}
