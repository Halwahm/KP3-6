using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Tasks : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject practicePanel, taskPanel;
    public TMP_Text taskNowTxt;
    [SerializeField] internal TableScript table;
    [SerializeField] private GameObject _clearButton;

    public enum States
    {
        Started,
        Now,
        Completed
    }
    public States state = States.Started;
    internal TasksNums currentTask = TasksNums.NULL;

    private void Awake()
    {
        _clearButton.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTask = TasksNums.FirstTask;
    }
}
