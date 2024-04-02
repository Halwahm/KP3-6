using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WtrBtnScr : MonoBehaviour
{
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

    [SerializeField] private GameObject firstWater;
    private GameObject currentWater;

    private void Start()
    {
        DeactivateOtherButtons();
    }

    public void StartAnimationSequence()
    {
        StartCoroutine(AnimationSequence());
        StartCoroutine(StartTimer(timerDuration));
    }

    private IEnumerator AnimationSequence()
    {
        // Открываем крышку
        lidAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        Destroy(firstWater);

        // Перемещаем чашу к ёмкости
        bowlAnimator.SetTrigger("MoveToContainer");
        yield return new WaitForSeconds(1f);

        // Создаем и перемещаем префаб с водой
        currentWater = Instantiate(waterPrefab, waterSpawnPosition, Quaternion.identity);
        currentWater.transform.localScale = waterSize;

        // Возвращаем чашу с перловкой назад
        bowlAnimator.SetTrigger("MoveBack");
        yield return new WaitForSeconds(1f);

        // Закрываем крышку
        lidAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
    }

    private void DeactivateOtherButtons()
    {
        foreach (Button button in _diactotherButtons)
        {
            button.interactable = false;
        }
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
        yield return new WaitForSeconds(1.3f);
        lidAnimator.SetTrigger("Close");
        timerText.text = "Вода вскипятилась, загляните в таблицу";
        yield return new WaitForSeconds(5f);
        timerText.text = "";
    }
}
