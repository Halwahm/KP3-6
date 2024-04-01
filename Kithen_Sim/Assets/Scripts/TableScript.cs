using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class TableScript : MonoBehaviour
{
    [SerializeField] private int fill;
    [SerializeField] internal GameObject table;
    private GameObject[] _tempCells, _kallCells;
    internal List<Text> tempValue = new List<Text>();
    internal List<Text> kallValue = new List<Text>();
    public ShowTable showTable;

    private void Start()
    {
        showTable._table = table;
        _tempCells = new GameObject[3];
        _kallCells = new GameObject[3];
        for (byte i = 1; i <= fill; i++)
        {
            _tempCells[i - 1] = GameObject.Find("T" + i);
            _kallCells[i - 1] = GameObject.Find("R" + i);
        }

        foreach (GameObject tempCell in _tempCells)
        {
            Text tempText = tempCell.GetComponent<Text>();
            if (tempText != null)
            {
                tempValue.Add(tempText);
            }
        }

        foreach (GameObject kallCell in _kallCells)
        {
            Text resistText = kallCell.GetComponent<Text>();
            if (resistText != null)
            {
                kallValue.Add(resistText);
            }
        }

        table.SetActive(false);
    }
}
