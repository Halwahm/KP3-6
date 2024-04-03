using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class TableScript : MonoBehaviour
{
    [SerializeField] private int fill;
    [SerializeField] internal GameObject table;
    private GameObject[] _tempCells, _kallCells, _kallKoefCells;
    internal List<Text> tempValue = new List<Text>();
    internal List<Text> kallValue = new List<Text>();
    internal List<Text> kallKoefValue = new List<Text>();
    public ShowTable showTable;

    private void Start()
    {
        showTable._table = table;
        _tempCells = new GameObject[3];
        _kallCells = new GameObject[3];
        _kallKoefCells = new GameObject[3];
        for (byte i = 1; i <= fill; i++)
        {
            _tempCells[i - 1] = GameObject.Find("T" + i);
            _kallCells[i - 1] = GameObject.Find("R" + i);
            _kallKoefCells[i - 1] = GameObject.Find("k" + i);
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

    public float GetCaloriesCoefficientByIndex(int index)
    {
        float coefficient = 0f; 
        if (index >= 0 && index < _kallKoefCells.Length)
        {
            Text coefficientText = _kallKoefCells[index].GetComponentInChildren<Text>();
            if (coefficientText != null && float.TryParse(coefficientText.text, NumberStyles.Any, 
                CultureInfo.InvariantCulture, out coefficient))
                return coefficient;
        }
        return coefficient; 
    }
}
