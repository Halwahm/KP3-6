using UnityEngine;
using System.Collections;
using TMPro;

public class BuckWheat : MonoBehaviour
{
    internal bool isDone = false;
    private BuckAllWater buckAllWater;
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
    [SerializeField] private Vector3 newPorridgeSpawnPosition;
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Material newMaterial;
    [SerializeField] private BuckAllWater allWaterScript;
    [SerializeField] private KrishkaScript krishkaScript;
    [SerializeField] private GameObject BW;
    [SerializeField] private Animator BWAnim;

    private GameObject currentPorridge;
    private GameObject currentWater;
    private GameObject NewPorridge;
    private Material originalMaterial;

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
        bool isOpen = krishkaScript.krishkaAnim.GetBool("Open");
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    timerText.text = "";
                    if (isOpen && fireButton.temperature != 0f && !hasSinglePressOccurred 
                        && buckAllWater.IsMoveBackTriggerSet && buckAllWater.isDone)
                    {
                        currentPorridge.SetActive(false);
                        StartCoroutine(StartTimer(timerDuration));
                        allWaterScript.ResetMaterial();
                        hasSinglePressOccurred = true;
                        buckWheat.SetTrigger("MoveToContainer");
                        StartCoroutine(MoveBackAfterDelay(1.5f));
                    }
                    else if (isOpen && hasSinglePressOccurred && fireButton.temperature != 0f)
                        StartCoroutine(MoveOneMore(1.5f));
                    else if (!isOpen && hasSinglePressOccurred && fireButton.temperature != 0f)
                        timerText.text = "Откройте крышку!";
                    else if (!isOpen && fireButton.temperature != 0f)
                        timerText.text = "Откройте крышку!";
                    else if (fireButton.temperature == 0f)
                        timerText.text = "Зажгите огонь!";
                    else if (!buckAllWater.IsMoveBackTriggerSet)
                        timerText.text = "Налейте воды и вскипятите";
                    else if (!buckAllWater.isDone)
                        timerText.text = "Сначала вскипятите воду";
                }
            }
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(0.45f);
        BW.SetActive(true);
        BWAnim.SetBool("PorridgeDown", true);
        yield return new WaitForSeconds(delay);
        BW.SetActive(false);
        buckWheat.SetTrigger("MoveBack");
        NewPorridge = Instantiate(prefabToInstantiate, newPorridgeSpawnPosition, Quaternion.identity);
        NewPorridge.SetActive(true);
    }

    private IEnumerator MoveOneMore(float delay)
    {
        buckWheat.SetTrigger("MoveOneMore");
        yield return new WaitForSeconds(0.8f);
        NewPorridge.SetActive(false);
        allWaterScript.ResetMaterial();
        buckAllWater.OnMoveBackCompleted();
        yield return new WaitForSeconds(delay);
        currentPorridge = Instantiate(prefabToInstantiate, PorridgespawnPosition, Quaternion.identity);
        currentPorridge.SetActive(true);
        currentWater = Instantiate(waterBuck, waterSpawnPosition, Quaternion.identity);
        currentWater.transform.localScale = waterSize;
        vaporParticleSystem.Play();
        float temperature = fireButton.temperature;
        timerText.text = "Гречка приготовилась,загляните в таблицу";
        isDone = true;
        float caloriesCoefficient = tableScript.GetCaloriesCoefficientByIndex(2);
        float calories = temperature * caloriesCoefficient * 0.79f;
        FillTable(2, temperature, calories);
        yield return new WaitForSeconds(2.5f);
        timerText.text = "Вскипятите воду для ещё одной каши";
        SetMaterial();
        yield return new WaitForSeconds(2f);
        timerText.text = "";
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
        timerText.text = "Гречка готова, можно доставать";
        allWaterScript.SetMaterial();
    }

    private void FillTable(int rowIndex, float temperature, float calories)
    {
        if (tableScript.tempValue.Count > rowIndex && tableScript.kallValue.Count > rowIndex)
        {
            tableScript.tempValue[rowIndex].text = temperature.ToString();
            tableScript.kallValue[rowIndex].text = calories.ToString();
        }
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
}
