using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuckPearl : MonoBehaviour
{
    private BuckAllWater buckAllWater;
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
    [SerializeField] private Vector3 newPorridgeSpawnPosition;

    private GameObject currentPorridge;
    private GameObject currentWater;
    private GameObject NewPorridge;

    private bool hasSinglePressOccurred = false;

    private void Awake()
    {
        vaporParticleSystem.Stop();
        currentPorridge = Instantiate(prefabToInstantiate, PorridgespawnPosition, Quaternion.identity);
    }

    private void Start()
    {
        buckAllWater = FindObjectOfType<BuckAllWater>();
        tableScript = FindObjectOfType<TableScript>();
        fireButton = FindObjectOfType<FireButtonScr>();
        currentPorridge.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    timerText.text = "";
                    if (!hasSinglePressOccurred)
                    {
                        currentPorridge.SetActive(false);
                        StartCoroutine(StartTimer(timerDuration));
                        hasSinglePressOccurred = true;
                        buckPearl.SetTrigger("MoveToContainer");
                        StartCoroutine(MoveBackAfterDelay(1.5f));
                    }
                    else
                        StartCoroutine(MoveOneMore(1.5f));
                }
            }
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buckPearl.SetTrigger("MoveBack");
        NewPorridge = Instantiate(prefabToInstantiate, newPorridgeSpawnPosition, Quaternion.identity);
        NewPorridge.SetActive(true);
    }

    private IEnumerator MoveOneMore(float delay)
    {
        buckPearl.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(0.8f);
        NewPorridge.SetActive(false);
        buckAllWater.OnMoveBackCompleted();
        yield return new WaitForSeconds(delay);
        currentPorridge.SetActive(true);
        currentWater = Instantiate(waterBuck, waterSpawnPosition, Quaternion.identity);
        currentWater.transform.localScale = waterSize;
        vaporParticleSystem.Play();
        float temperature = fireButton.temperature;
        timerText.text = "Перловка приготовилась,загляните в таблицу";
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
        timerText.text = "Перловка готова, можно доставать";
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
