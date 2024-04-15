using UnityEngine;
using UnityEngine.EventSystems;

public class PracticaScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _view;
    [SerializeField] private GameObject _TaskPanel;

    private bool isTaskPanelActive = false;

    void Start()
    {
        _view.SetActive(false);
        _TaskPanel.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        isTaskPanelActive = !isTaskPanelActive;

        if (isTaskPanelActive)
        {
            _TaskPanel.SetActive(true);
            _view.SetActive(true);
        }
        else
        {
            _TaskPanel.SetActive(false);
            _view.SetActive(false);
        }
    }
}
