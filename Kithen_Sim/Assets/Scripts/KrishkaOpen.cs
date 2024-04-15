using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrishkaOpen : MonoBehaviour
{
    [SerializeField] private Animator krishkaOpen;

    private void Start()
    {
        krishkaOpen.SetBool("Open", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            krishkaOpen.SetBool("Open", true);
            krishkaOpen.SetBool("Close", false);
        }
    }
}
