using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] internal Animator doorAnimator;
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Material newMaterial;
    internal bool isDoorOpen = false;
    private Material originalMaterial;

    private void Start()
    {
        SetMaterial();
        doorAnimator.SetBool("doorOpen", false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                    ToggleDoorState();
            }
        }
    }

    private void ToggleDoorState()
    {
        isDoorOpen = !isDoorOpen;
        doorAnimator.SetBool("doorOpen", isDoorOpen);
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
