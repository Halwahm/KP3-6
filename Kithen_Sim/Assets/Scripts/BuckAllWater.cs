using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuckAllWater : MonoBehaviour
{
    public event Action MoveBackCompleted;

    [SerializeField] internal Animator buckWater;
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Vector3 WaterspawnPosition;
    [SerializeField] private Vector3 WaterSize;
    [SerializeField] private Vector3 newWaterSpawnPosition;
    [SerializeField] private Vector3 newWaterSize;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 5f;
    [SerializeField] private KrishkaScript krishkaScript;
    [SerializeField] private FireButtonScr fireButton;
    [SerializeField] private BuckWater waterScript;
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Material newMaterial;
    [SerializeField] private GameObject SectargetGameObject;

    private GameObject currentWater;
    private GameObject NewWater;
    private Material originalMaterial;
    private BuckWater BWScript;
    internal bool isDone = false;
    internal bool isDoneSec = false;

    internal bool IsMoveBackTriggerSet
    {
        get { return buckWater.GetCurrentAnimatorStateInfo(0).IsName("AllWater_MoveBack"); }
    }

    private void Awake()
    {
        currentWater = Instantiate(prefabToInstantiate, WaterspawnPosition, Quaternion.identity);
        currentWater.transform.localScale = WaterSize;
    }

    private void Start()
    {
        BWScript = FindObjectOfType<BuckWater>();
        currentWater.SetActive(true);
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
                    if (isOpen && fireButton.temperature != 0f && BWScript.isDone)
                    {
                        buckWater.SetTrigger("MoveToContainer");
                        StartCoroutine(MoveBackAfterDelay(1.5f));
                        waterScript.ResetMaterial();
                        currentWater.SetActive(false);
                        StartCoroutine(StartTimer(timerDuration));
                    }
                    else if (!isOpen && fireButton.temperature != 0f)
                        timerText.text = "Откройте крышку";
                    else if (fireButton.temperature == 0f)
                        timerText.text = "Зажгите огонь!";
                    else if (!BWScript.isDone)
                        timerText.text = "Сначала вскипятите воду для питья!";
                }
            }
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buckWater.SetTrigger("MoveBack");
        NewWater = Instantiate(prefabToInstantiate, newWaterSpawnPosition, Quaternion.identity);
        NewWater.transform.localScale = newWaterSize;
        yield return new WaitForSeconds(delay);
        currentWater.SetActive(true);
        MoveBackCompleted?.Invoke();
    }

    private bool isMaterialSet = false;
    private bool isSecondMaterialSet = false;

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
        if (!isMaterialSet)
        {
            timerText.text = "Вода вскипела, можно открыть крышку и засыпать кашу";
            SetMaterial();
            isMaterialSet = true;
            isDone = true;
        }
        else if (!isSecondMaterialSet)
        {
            timerText.text = "Вода вскипела, можно открыть крышку и засыпать кашу";
            SetSecMaterial();
            isSecondMaterialSet = true;
            isDoneSec = true;
        }
        
    }

    internal void OnMoveBackCompleted()
    {
        NewWater.SetActive(false);
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

    internal void SetSecMaterial()
    {
        Renderer renderer2 = SectargetGameObject.GetComponent<Renderer>();
        if (renderer2 != null)
        {
            if (originalMaterial == null)
                originalMaterial = renderer2.material;
            renderer2.material = newMaterial;
        }
    }

    internal void ResetMaterial()
    {
        if (targetGameObject != null && originalMaterial != null)
        {
            Renderer renderer = targetGameObject.GetComponent<Renderer>();
            Renderer renderer2 = SectargetGameObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material = originalMaterial;
            if (renderer2 != null)
                renderer2.material = originalMaterial;
        }
    }
}
