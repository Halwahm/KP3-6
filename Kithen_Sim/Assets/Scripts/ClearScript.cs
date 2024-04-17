using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal GameObject table;
    private GameObject[] _tempCells, _kallCells;
    [SerializeField] private GameObject _clearButton;
    [SerializeField] private TMP_Text congratulationsText;

    private void Awake()
    {
        _clearButton.SetActive(false);
        _tempCells = GameObject.FindGameObjectsWithTag("Temperature");
        _kallCells = GameObject.FindGameObjectsWithTag("Kallor");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (GameObject tempCell in _tempCells)
        {
            if (tempCell.TryGetComponent<Text>(out var tempText))
            {
                tempText.text = "-";
            }
        }
        foreach (GameObject KallCells in _kallCells)
        {
            if (KallCells.TryGetComponent<Text>(out var resistText))
            {
                resistText.text = "-";
            }
        }
        congratulationsText.text = "";
        //_clearButton.SetActive(false);
    }
}
