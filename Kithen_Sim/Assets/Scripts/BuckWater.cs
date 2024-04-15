using System.Collections;
using TMPro;
using UnityEngine;

public class BuckWater : MonoBehaviour
{
    [SerializeField] private Animator buckWater;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Vector3 WaterspawnPosition;
    [SerializeField] private Vector3 WaterSize;
    [SerializeField] private ParticleSystem vaporParticleSystem;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 12f;

    private TableScript tableScript;
    private FireButtonScr fireButton;

    private GameObject currentWater;
    internal bool isBPressed = false;
    private bool hasSinglePressOccurred = false;

    private void Start()
    {
        tableScript = FindObjectOfType<TableScript>();
        fireButton = FindObjectOfType<FireButtonScr>();
    }

    private void Awake()
    {
        vaporParticleSystem.Stop();
        currentWater = Instantiate(waterPrefab, WaterspawnPosition, Quaternion.identity);
        currentWater.transform.localScale = WaterSize;
        currentWater.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            isBPressed = true;
            if (!hasSinglePressOccurred)
            {
                StartCoroutine(StartTimer(timerDuration));
                hasSinglePressOccurred = true;
                buckWater.SetTrigger("MoveToContainer");
                StartCoroutine(MoveBackAfterDelay(1f));
                currentWater.SetActive(false);
            }
            else
            {
                StartCoroutine(MoveOneMore(1f));
            }
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            isBPressed = false;
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buckWater.SetTrigger("MoveBack");
    }

    private IEnumerator MoveOneMore(float delay)
    {
        buckWater.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(1.5f);
        currentWater.SetActive(true);
        vaporParticleSystem.Play();
        timerText.text = "¬ода вскип€тилась, загл€ните в таблицу";
        float temperature = fireButton.temperature;
        float caloriesCoefficient = tableScript.GetCaloriesCoefficientByIndex(1);
        float calories = temperature * caloriesCoefficient;
        FillTable(1, temperature, calories);
        yield return new WaitForSeconds(2.5f);
        timerText.text = "";
    }

    private void FillTable(int rowIndex, float temperature, float calories)
    {
        if (tableScript.tempValue.Count > rowIndex && tableScript.kallValue.Count > rowIndex)
        {
            tableScript.tempValue[rowIndex].text = temperature.ToString();
            tableScript.kallValue[rowIndex].text = calories.ToString();
        }
    }

    private IEnumerator StartTimer(float duration)
    {
        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }
        timerText.text = "¬ода вскип€тилась, можно переливать";
    }
}
