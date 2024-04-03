using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal GameObject table;
    private GameObject[] _tempCells, _kallCells;
    [SerializeField] private GameObject _clearButton;
    [SerializeField] private Button _activateButton;
    [SerializeField] internal ParticleSystem[] vaporParticleSystems;
    [SerializeField] private TMP_Text congratulationsText;
    private List<GameObject> allInstantiatedPrefabs = new List<GameObject>();

    private void Awake()
    {
        _clearButton.SetActive(false);
        _tempCells = GameObject.FindGameObjectsWithTag("Temperature");
        _kallCells = GameObject.FindGameObjectsWithTag("Kallor");

        WtrBtnScr wtrBtnScr = FindObjectOfType<WtrBtnScr>();
        if (wtrBtnScr != null)
            wtrBtnScr.onPrefabInstantiated += AddToAllInstantiatedPrefabs;

        PearlBtnScr pearlBtnScr = FindObjectOfType<PearlBtnScr>();
        if (pearlBtnScr != null)
            pearlBtnScr.onPrefabInstantiated += AddToAllInstantiatedPrefabs;

        BWBtnScr bwBtnScr = FindObjectOfType<BWBtnScr>();
        if (bwBtnScr != null)
            bwBtnScr.onPrefabInstantiated += AddToAllInstantiatedPrefabs;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (GameObject tempCell in _tempCells)
        {
            if (tempCell.TryGetComponent<Text>(out var tempText))
            {
                tempText.text = "-";
            }
        }
        foreach (GameObject KallCells in _kallCells)
        {
            if (KallCells.TryGetComponent<Text>(out var resistText))
            {
                resistText.text = "-";
            }
        }
        foreach(ParticleSystem PS in vaporParticleSystems)
        {
            PS.Stop();
        }
        ClearAllInstantiatedPrefabs();
        congratulationsText.text = "";
        _activateButton.interactable = true;
        _clearButton.SetActive(false);
    }

    private void ClearAllInstantiatedPrefabs()
    {
        foreach (GameObject prefab in allInstantiatedPrefabs)
        {
            Destroy(prefab);
        }
        allInstantiatedPrefabs.Clear();
    }

    public void AddToAllInstantiatedPrefabs(GameObject prefab)
    {
        allInstantiatedPrefabs.Add(prefab);
    }
}
