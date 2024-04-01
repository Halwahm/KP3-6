using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PracticaScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _TaskPanel;

    private bool isTaskPanelActive = false;

    void Start()
    {
        _TaskPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isTaskPanelActive = !isTaskPanelActive;

        if (isTaskPanelActive)
        {
            _TaskPanel.SetActive(true);
        }
        else
        {
            _TaskPanel.SetActive(false);
        }
    }
}
