using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal GameObject table;
    private GameObject[] _tempCells, _kallCells;
    [SerializeField] private Tasks _tasks;

    private void Awake()
    {
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

        _tasks.state = Tasks.States.Started;
        _tasks.currentTask = Tasks.TasksNums.NULL;
    }
}
