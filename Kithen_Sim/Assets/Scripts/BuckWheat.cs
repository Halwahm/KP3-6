using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class BuckWheat : MonoBehaviour
{
    internal event Action<GameObject> onPrefabInstantiated;
    private TableScript tableScript;
    private FireButtonScr fireButton; 
    [SerializeField] private ParticleSystem vaporParticleSystem;
    [SerializeField] private Animator buckWheat;
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Vector3 PorridgespawnPosition;
    [SerializeField] private GameObject waterBuck;
    [SerializeField] private Vector3 waterSpawnPosition;
    [SerializeField] private Vector3 waterSize;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 5f;

    private GameObject currentPorridge;
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
        currentPorridge = Instantiate(prefabToInstantiate, PorridgespawnPosition, Quaternion.identity);
        currentPorridge.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBPressed = true;

            if (!hasSinglePressOccurred)
            {
                StartCoroutine(StartTimer(timerDuration));
                hasSinglePressOccurred = true;
                buckWheat.SetTrigger("MoveToContainer");
                StartCoroutine(MoveBackAfterDelay(1f));
                currentPorridge.SetActive(false);
            }
            else
            {
                StartCoroutine(MoveOneMore(1.5f));
            }
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            isBPressed = false;
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buckWheat.SetTrigger("MoveBack");
    }

    private IEnumerator MoveOneMore(float delay)
    {
        buckWheat.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(delay);
        currentPorridge.SetActive(true);
        currentWater = Instantiate(waterBuck, waterSpawnPosition, Quaternion.identity);
        currentWater.transform.localScale = waterSize;
        vaporParticleSystem.Play();
        float temperature = fireButton.temperature;
        timerText.text = "Гречка приготовилась,загляните в таблицу";
        float caloriesCoefficient = tableScript.GetCaloriesCoefficientByIndex(2);
        float calories = temperature * caloriesCoefficient * 0.79f;
        FillTable(2, temperature, calories);
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
        timerText.text = "Гречка готова, можно доставать";
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
