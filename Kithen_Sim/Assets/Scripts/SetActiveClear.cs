using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetActiveClear : MonoBehaviour
{
    [SerializeField] private GameObject _clearButton;
    [SerializeField] private BuckWater buckWater;
    [SerializeField] private BuckWheat buckWheat;
    [SerializeField] private BuckPearl buckPearl;

    private void Update()
    {
        if (buckWater.isDone && buckWheat.isDone && buckPearl.isDone)
            _clearButton.SetActive(true);
    }
}
