using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Animator doorOpen;

    private void Start()
    {
        doorOpen.SetBool("doorOpen", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            doorOpen.SetBool("doorOpen", true);
            doorOpen.SetBool("doorClose", false);
        }
    }
}
