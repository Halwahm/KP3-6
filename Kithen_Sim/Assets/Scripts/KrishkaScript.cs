using System;
using UnityEngine;

public class KrishkaScript : MonoBehaviour
{
    [SerializeField] internal Animator krishkaAnim;
    internal bool isOpen = false;

    private void Start()
    {
        krishkaAnim.SetBool("Open", isOpen);
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
                    ToggleKrishkaState();
            }
        }
    }

    private void ToggleKrishkaState()
    {
        isOpen = !isOpen;
        krishkaAnim.SetBool("Open", isOpen);
    }
}
