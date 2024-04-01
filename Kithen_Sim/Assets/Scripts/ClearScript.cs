using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal GameObject table;
    private GameObject[] _tempCells, _resistCells, _densCells;
    [SerializeField] private Tasks _tasks;

    private void Awake()
    {
        _tempCells = GameObject.FindGameObjectsWithTag("Temperature");
        _resistCells = GameObject.FindGameObjectsWithTag("Resistance");
        _densCells = GameObject.FindGameObjectsWithTag("Density");
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

        foreach (GameObject resistCell in _resistCells)
        {
            if (resistCell.TryGetComponent<Text>(out var resistText))
            {
                resistText.text = "-";
            }
        }

        foreach (GameObject densCell in _densCells)
        {
            if (densCell.TryGetComponent<Text>(out var densText))
            {
                densText.text = "-";
            }
        }
/*
        _tasks.state = Tasks.States.Started;
        _tasks.currentTask = Tasks.TasksNums.NULL;*/
    }
}
