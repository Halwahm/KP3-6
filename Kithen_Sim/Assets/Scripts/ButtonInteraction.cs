using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private GameObject[] installationElements; 
    [SerializeField] private string _aboutInst;

    [SerializeField] private Material highlightMaterial;
    private Material[] originalMaterials;

    [SerializeField] private Transform targetCamera;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private Vector3 cameraRotation;

    bool isMoving = false;
    float moveSpeed = 7f;

    private void Start()
    {
        originalMaterials = new Material[installationElements.Length];
        int index = 0;

        foreach (var element in installationElements)
        {
            originalMaterials[index] = element.GetComponent<Renderer>().material;
            index++;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector3.Lerp(targetCamera.position, cameraPosition, moveSpeed * Time.deltaTime);
            Quaternion newRotation = Quaternion.Slerp(targetCamera.rotation, Quaternion.Euler(cameraRotation), moveSpeed * Time.deltaTime);

            targetCamera.position = newPosition;
            targetCamera.rotation = newRotation;

            if (Vector3.Distance(targetCamera.position, cameraPosition) < 0.01f && 
                Quaternion.Angle(targetCamera.rotation, Quaternion.Euler(cameraRotation)) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.text = _aboutInst;
        foreach (var element in installationElements)
        {
            element.GetComponent<Renderer>().material = highlightMaterial;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        int index = 0;
        foreach (var element in installationElements)
        {
            element.GetComponent<Renderer>().material = originalMaterials[index];
            index++;
        }
        infoText.text = "";
    }

    public void OnPointerClick()
    {
        if (!isMoving)
        {
            isMoving = true;
        }
    }
}
