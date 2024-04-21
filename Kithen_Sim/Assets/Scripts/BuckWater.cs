using System.Collections;
using TMPro;
using UnityEngine;

public class BuckWater : MonoBehaviour
{
    internal bool isDone = false;
    [SerializeField] private Animator buckWater;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Vector3 WaterspawnPosition;
    [SerializeField] private Vector3 WaterSize;
    [SerializeField] private ParticleSystem vaporParticleSystem;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 12f;
    [SerializeField] private Vector3 newWaterSpawnPosition;
    [SerializeField] private Vector3 newWaterSize;
    [SerializeField] private KrishkaScript krishkaScript;
    [SerializeField] private FireButtonScr FireScript;
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Material newMaterial;
    [SerializeField] internal GameObject prefabWaterFromBowl;
    [SerializeField] private Vector3 CoordwaterFromBowl;
    [SerializeField] private Vector3 RotatedwaterFromBowl;
    [SerializeField] private GameObject WtFromBowl;

    private TableScript tableScript;
    private FireButtonScr fireButton;

    private GameObject waterFromBowl;
    private GameObject currentWater;
    private GameObject NewWater;
    private bool hasSinglePressOccurred = false;
    private Material originalMaterial;

    private void Start()
    {
        tableScript = FindObjectOfType<TableScript>();
        fireButton = FindObjectOfType<FireButtonScr>();
        currentWater.SetActive(true);
    }

    private void Awake()
    {
        vaporParticleSystem.Stop();
        currentWater = Instantiate(waterPrefab, WaterspawnPosition, Quaternion.identity);
        currentWater.transform.localScale = WaterSize;
    }

    private void Update()
    {
        bool isOpen = krishkaScript.krishkaAnim.GetBool("Open");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (isOpen && fireButton.temperature != 0f && !hasSinglePressOccurred)
                    {
                        hasSinglePressOccurred = true;
                        buckWater.SetTrigger("MoveToContainer");
                        FireScript.ResetMaterial();
                        StartCoroutine(MoveBackAfterDelay(1.8f));
                        currentWater.SetActive(false);
                        StartCoroutine(StartTimer(timerDuration));
                    }
                    else if (isOpen && hasSinglePressOccurred && fireButton.temperature != 0f)
                        StartCoroutine(MoveOneMore(1f));
                    else if (!isOpen && hasSinglePressOccurred && fireButton.temperature != 0f)
                        timerText.text = "Откройте крышку!";
                    else if (!isOpen && fireButton.temperature != 0f)
                        timerText.text = "Откройте крышку";
                    else if (fireButton.temperature == 0f)
                        timerText.text = "Зажгите огонь!";
                }
            }
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(0.7f);
        WtFromBowl.SetActive(true);
        waterFromBowl = Instantiate(prefabWaterFromBowl, CoordwaterFromBowl, Quaternion.Euler(RotatedwaterFromBowl));
        yield return new WaitForSeconds(delay);
        WtFromBowl.SetActive(false);
        Destroy(waterFromBowl);
        buckWater.SetTrigger("MoveBack");
        NewWater = Instantiate(waterPrefab, newWaterSpawnPosition, Quaternion.identity);
        NewWater.transform.localScale = newWaterSize;
    }

    private IEnumerator MoveOneMore(float delay)
    {
        buckWater.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(0.8f);
        NewWater.SetActive(false);
        FireScript.ResetMaterial();
        yield return new WaitForSeconds(delay);
        currentWater.SetActive(true);
        vaporParticleSystem.Play();
        timerText.text = "Вода вскипелась, загляните в таблицу";
        //float temperature = fireButton.temperature;
        float temperature = 100f;
        float caloriesCoefficient = tableScript.GetCaloriesCoefficientByIndex(1);
        float calories = temperature * caloriesCoefficient;
        FillTable(1, temperature, calories);
        yield return new WaitForSeconds(2.5f);
        timerText.text = "Теперь вскипятите воду для каши";
        isDone = true;
        SetMaterial();
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
        bool isOpen = krishkaScript.krishkaAnim.GetBool("Open");
        while (isOpen)
        {
            timerText.text = "Закройте крышку";
            yield return null; // Подождать один кадр
            isOpen = krishkaScript.krishkaAnim.GetBool("Open");
        }

        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }
        timerText.text = "Вода вскипела, можно переливать";
        FireScript.SetMaterial();
    }

    internal void SetMaterial()
    {
        Renderer renderer = targetGameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (originalMaterial == null)
                originalMaterial = renderer.material;
            renderer.material = newMaterial;
        }
    }

    internal void ResetMaterial()
    {
        if (targetGameObject != null && originalMaterial != null)
        {
            Renderer renderer = targetGameObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material = originalMaterial;
        }
    }
}
