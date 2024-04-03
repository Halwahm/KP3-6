using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class WtrBtnScr : MonoBehaviour
{
    internal event Action<GameObject> onPrefabInstantiated;

    private TableScript tableScript;
    private FireButtonScr fireButton;
    private GameObject currentWater;

    [SerializeField] private Button[] _diactotherButtons;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Vector3 waterSpawnPosition;
    [SerializeField] private Vector3 waterSize;
    [SerializeField] private Vector3 newWaterSpawnPosition;
    [SerializeField] private Vector3 newWaterSize;
    [SerializeField] private Vector3 newWaterRotation;
    [SerializeField] private Animator lidAnimator;
    [SerializeField] private Animator bowlAnimator;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 12f;
    [SerializeField] private ParticleSystem vaporParticleSystem;
    [SerializeField] private TMP_Text congratulationsText;
    [SerializeField] private GameObject firstWater;
    [SerializeField] private GameObject _clearButton;

    private void Awake()
    {
        vaporParticleSystem.Stop();
    }

    private void Start()
    {
        tableScript = FindObjectOfType<TableScript>();
        fireButton = FindObjectOfType<FireButtonScr>();
        DeactivateOtherButtons();
    }

    public void StartAnimationSequence()
    {
        StartCoroutine(AnimationSequence());
        StartCoroutine(StartTimer(timerDuration));
    }

    private IEnumerator AnimationSequence()
    {
        lidAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        Destroy(firstWater);

        bowlAnimator.SetTrigger("MoveToContainer");
        yield return new WaitForSeconds(1f);

        currentWater = Instantiate(waterPrefab, waterSpawnPosition, Quaternion.identity);
        currentWater.transform.localScale = waterSize;

        bowlAnimator.SetTrigger("MoveBack");
        yield return new WaitForSeconds(1f);

        lidAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator StartTimer(float duration)
    {
        DeactivateOtherButtons();
        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }
        lidAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(1.5f);
        bowlAnimator.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(2.5f);
        Destroy(currentWater);
        currentWater = Instantiate(waterPrefab, newWaterSpawnPosition, Quaternion.Euler(newWaterRotation));
        currentWater.transform.localScale = newWaterSize;
        onPrefabInstantiated?.Invoke(currentWater);
        yield return new WaitForSeconds(1.3f);
        lidAnimator.SetTrigger("Close");
        vaporParticleSystem.Play();
        timerText.text = "Вода вскипятилась, загляните в таблицу";

        float temperature = fireButton.temperature;
        float caloriesCoefficient = tableScript.GetCaloriesCoefficientByIndex(0);
        float calories = temperature * caloriesCoefficient * 0f;
        FillTable(1, temperature, calories);
        congratulationsText.text = "Поздравляем. Вы накормили целую роту!";

        yield return new WaitForSeconds(2.5f);
        timerText.text = "";
        _clearButton.SetActive(true);
    }

    private void FillTable(int rowIndex, float temperature, float calories)
    {
        if (tableScript.tempValue.Count > rowIndex && tableScript.kallValue.Count > rowIndex)
        {
            tableScript.tempValue[rowIndex].text = temperature.ToString();
            tableScript.kallValue[rowIndex].text = calories.ToString();
        }
    }

    private void DeactivateOtherButtons()
    {
        foreach (Button button in _diactotherButtons)
        {
            button.interactable = false;
        }
    }
}
