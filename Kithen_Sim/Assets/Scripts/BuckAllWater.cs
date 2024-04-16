using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuckAllWater : MonoBehaviour
{
    public event Action MoveBackCompleted;

    [SerializeField] private Animator buckWater;
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Vector3 WaterspawnPosition;
    [SerializeField] private Vector3 WaterSize;
    [SerializeField] private Vector3 newWaterSpawnPosition;
    [SerializeField] private Vector3 newWaterSize;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timerDuration = 5f;
    [SerializeField] private KrishkaScript krishkaScript;
    [SerializeField] private FireButtonScr fireButton;

    private GameObject currentWater;
    private GameObject NewWater;

    private void Awake()
    {
        currentWater = Instantiate(prefabToInstantiate, WaterspawnPosition, Quaternion.identity);
        currentWater.transform.localScale = WaterSize;
    }

    private void Start()
    {
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
                    if (isOpen && fireButton.temperature != 0f)
                    {
                        buckWater.SetTrigger("MoveToContainer");
                        StartCoroutine(MoveBackAfterDelay(1.5f));
                        currentWater.SetActive(false);
                        StartCoroutine(StartTimer(timerDuration));
                    }
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
        yield return new WaitForSeconds(delay);
        buckWater.SetTrigger("MoveBack");
        NewWater = Instantiate(prefabToInstantiate, newWaterSpawnPosition, Quaternion.identity);
        NewWater.transform.localScale = newWaterSize;
        yield return new WaitForSeconds(delay);
        currentWater.SetActive(true);
        MoveBackCompleted?.Invoke();
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
        timerText.text = "Вода вскипела, можно открыть крышку и засыпать кашу";
    }

    internal void OnMoveBackCompleted()
    {
        NewWater.SetActive(false);
    }
}
