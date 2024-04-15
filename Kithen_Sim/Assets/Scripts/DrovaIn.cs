using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrovaIn : MonoBehaviour
{
    [SerializeField] private Animator drovAnim; 

    private void Start()
    {
        drovAnim.SetBool("drovAnim", false);
    }

    private void OnMouseDown()
    {
        drovAnim.SetBool("drovAnim", true);
    }
}
