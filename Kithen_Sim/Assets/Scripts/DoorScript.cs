using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] internal Animator doorAnimator;
    private bool isDoorOpen = false;

    private void Start()
    {
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
}
