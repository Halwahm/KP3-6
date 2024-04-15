using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrishkaClose : MonoBehaviour
{
    [SerializeField] private Animator krishkaClose;

    private void Start()
    {
        krishkaClose.SetBool("Close", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            krishkaClose.SetBool("Close", true);
            krishkaClose.SetBool("Open", false);
        }
    }
}
