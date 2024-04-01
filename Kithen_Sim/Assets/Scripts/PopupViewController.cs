using UnityEngine;
using UnityEngine.EventSystems;

public class PopupViewController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _view;
    [SerializeField] private Transform targetCamera;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private Vector3 cameraRotation;

    bool isMoving = false;
    float moveSpeed = 7f;

    void Start()
    {
        _view.SetActive(false);
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector3.Lerp(targetCamera.position, cameraPosition, moveSpeed * Time.deltaTime);
            Quaternion newRotation = Quaternion.Slerp(targetCamera.rotation, Quaternion.Euler(cameraRotation), moveSpeed * Time.deltaTime);

            targetCamera.position = newPosition;
            targetCamera.rotation = newRotation;

            if (Vector3.Distance(targetCamera.position, cameraPosition) < 0.01f && Quaternion.Angle(targetCamera.rotation, Quaternion.Euler(cameraRotation)) < 0.01f)
            {
                isMoving = false; 
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _view.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _view.SetActive(false);
    }

    public void OnPointerClick()
    {
        if (!isMoving)
        {
            isMoving = true;
        }
    }

}
