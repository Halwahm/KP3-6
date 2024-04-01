/*using System.Globalization;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public partial class Tasks : MonoBehaviour
{
    private void Task1()
    {
        switch (state)
        {
            case States.Started:
                taskNowTxt.text = "Задание 1. Запишите в таблицу температуру проводника (свинец).";
                state = States.Now;
                break;
            case States.Now:
                if (_newValue.isAddButtonClicked && inputField.text != "")
                {
                    table.tempValue[0].text = inputField.text;
                    inputField.text = "";
                    _newValue.isAddButtonClicked = false;
                    state = States.Completed;
                }
                else
                {
                    _newValue.isAddButtonClicked = false;
                    state = States.Started;
                }
                break;
            case States.Completed:
                inputField.interactable = false;
                state = States.Started;
                currentTask = TasksNums.SecondTask;
                break;
        }
    }
    private void Task2()
    {
        switch (state)
        {
            case States.Started:
                taskNowTxt.text = "Задание 2. Включите омметр. Запишите в таблицу сопротивление проводника.";
                state = States.Now;
                break;
            case States.Now:
                if (_ohmmetr.ohmmetrState == OmmetrButtonScript.OhmmetrState.On)
                    inputField.interactable = true;
                if (_newValue.isAddButtonClicked && inputField.text != "")
                {
                    table.resistValue[0].text = inputField.text;
                    inputField.text = "";
                    if (float.TryParse(table.resistValue[0].text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out float resistanceValue))
                    {
                        float densityValue = resistanceValue * 0.000309f / 0.21f;
                        table.densValue[0].text = densityValue.ToString(CultureInfo.InvariantCulture);
                    }
                    _newValue.isAddButtonClicked = false;
                    state = States.Completed;
                }
                else
                {
                    _newValue.isAddButtonClicked = false;
                    state = States.Started;
                }
                break;
            case States.Completed:
                state = States.Started;
                currentTask = TasksNums.ThirdTask;
                break;
        }
    }
    private void Task3()
    {
        switch (state)
        {
            case States.Started:
                taskNowTxt.text = "Задание 3. Включите горелку и нагрейте металл до 90 °С.";
                state = States.Now;
                break;
            case States.Now:
                inputField.interactable = false;
                if (burner.burnerState == BurnerOnScript.BurnerState.On)
                {
                    _newValue.isAddButtonClicked = false;
                    state = States.Completed;
                }
                break;
            case States.Completed:
                state = States.Started;
                currentTask = TasksNums.FourthTask;
                break;
        }
    }
    private void Task4()
    {
        switch (state)
        {
            case States.Started:
                taskNowTxt.text = "Задание 4. Запишите в таблицу температуру проводника.";
                state = States.Now;
                break;
            case States.Now:
                inputField.interactable = true;
                if (_newValue.isAddButtonClicked && inputField.text != "")
                {
                    table.tempValue[1].text = inputField.text;
                    inputField.text = "";
                    _newValue.isAddButtonClicked = false;
                    state = States.Completed;
                }
                else
                {
                    _newValue.isAddButtonClicked = false;
                    state = States.Started;
                }
                break;
            case States.Completed:
                state = States.Started;
                currentTask = TasksNums.FifthTask;
                break;
        }
    }
    private void Task5()
    {
        switch (state)
        {
            case States.Started:
                taskNowTxt.text = "Задание 5. Запишите в таблицу сопротивление проводника.";
                state = States.Now;
                break;
            case States.Now:
                if (_newValue.isAddButtonClicked && inputField.text != "")
                {
                    _newValue.isAddButtonClicked = false;
                    table.resistValue[1].text = inputField.text;
                    inputField.text = "";
                    if (float.TryParse(table.resistValue[1].text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out float resistanceValue))
                    {
                        float densityValue = resistanceValue * 0.000309f / 0.21f;
                        table.densValue[1].text = densityValue.ToString(CultureInfo.InvariantCulture);
                        state = States.Completed;
                    }
                    else
                    {
                        _newValue.isAddButtonClicked = false;
                        state = States.Started;
                    }
                }

                break;
            case States.Completed:
                state = States.Started;
                currentTask = TasksNums.SixthTask;
                break;
        }
    }
}*/