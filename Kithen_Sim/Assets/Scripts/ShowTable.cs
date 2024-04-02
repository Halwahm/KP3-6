using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal GameObject _table;
    private bool isTableActive = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        isTableActive = !isTableActive;
        if (isTableActive)
        {
            _table.SetActive(true);
        }
        else
        {
            _table.SetActive(false);
        }
    }
}
