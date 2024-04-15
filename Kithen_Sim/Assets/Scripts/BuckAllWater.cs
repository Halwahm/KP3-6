using System.Collections;
using UnityEngine;

public class BuckAllWater : MonoBehaviour
{
    [SerializeField] private Animator buckWater;
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Vector3 WaterspawnPosition;
    [SerializeField] private Vector3 WaterSize;

    private GameObject currentWater;

    private void Awake()
    {
        currentWater = Instantiate(prefabToInstantiate, WaterspawnPosition, Quaternion.identity);
        currentWater.transform.localScale = WaterSize;
        currentWater.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            buckWater.SetTrigger("MoveToContainer");
            StartCoroutine(MoveBackAfterDelay(1f));
            currentWater.SetActive(false);
        }
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buckWater.SetTrigger("MoveBack");
        yield return new WaitForSeconds(1.5f);
        currentWater.SetActive(true);
    }
}
