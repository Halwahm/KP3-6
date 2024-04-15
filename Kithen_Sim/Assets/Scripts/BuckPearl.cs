using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuckPearl : MonoBehaviour
{
    internal event Action<GameObject> onPrefabInstantiated;
    private TableScript tableScript;
    private FireButtonScr fireButton;
    [SerializeField] private Animator buckPearl;
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Vector3 PorridgespawnPosition;
    [SerializeField] private GameObject waterBuck;
    [SerializeField] private Vector3 waterSpawnPosition;
    [SerializeField] private Vector3 waterSize;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 5f;
    [SerializeField] private ParticleSystem vaporParticleSystem;

    private GameObject currentPorridge;
    private GameObject currentWater;

    internal bool isBPressed = false;
    private bool hasSinglePressOccurred = false;

    private void Awake()
    {
        vaporParticleSystem.Stop();
        currentPorridge = Instantiate(prefabToInstantiate, PorridgespawnPosition, Quaternion.identity);
        currentPorridge.SetActive(true);
    }

    private void Start()
    {
        tableScript = FindObjectOfType<TableScript>();
        fireButton = FindObjectOfType<FireButtonScr>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isBPressed = true;

            if (!hasSinglePressOccurred)
            {
                StartCoroutine(StartTimer(timerDuration));
                hasSinglePressOccurred = true;
                buckPearl.SetTrigger("MoveToContainer");
                StartCoroutine(MoveBackAfterDelay(1f));
                currentPorridge.SetActive(false);
            }
            else
            {
                StartCoroutine(MoveOneMore(1f));
            }
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            isBPressed = false;
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buckPearl.SetTrigger("MoveBack");
    }

    private IEnumerator MoveOneMore(float delay)
    {
        buckPearl.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(1.5f);
        currentPorridge.SetActive(true);
        currentWater = Instantiate(waterBuck, waterSpawnPosition, Quaternion.identity);
        currentWater.transform.localScale = waterSize;
        vaporParticleSystem.Play();
        float temperature = fireButton.temperature;
        timerText.text = "�������� �������������,��������� � �������";
        float caloriesCoefficient = tableScript.GetCaloriesCoefficientByIndex(0);
        float calories = temperature * caloriesCoefficient * 0.79f;
        FillTable(0, temperature, calories);
        yield return new WaitForSeconds(2.5f);
        timerText.text = "";
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
        timerText.text = "�������� ������, ����� ���������";
    }

    private void FillTable(int rowIndex, float temperature, float calories)
    {
        if (tableScript.tempValue.Count > rowIndex && tableScript.kallValue.Count > rowIndex)
        {
            tableScript.tempValue[rowIndex].text = temperature.ToString();
            tableScript.kallValue[rowIndex].text = calories.ToString();
        }
    }
}