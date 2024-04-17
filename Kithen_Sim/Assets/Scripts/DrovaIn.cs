using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrovaIn : MonoBehaviour
{
    [SerializeField] private Animator drovAnim;
    [SerializeField] private Button nextButton;
    [SerializeField] private DoorScript doorScript;
    [SerializeField] private TMP_Text timerText;
    private DoorScript _dScript;

    private void Start()
    {
        _dScript = FindObjectOfType<DoorScript>();
        timerText.text = "�������� ������ � ��������� � �� �����";
        nextButton.interactable = false;
        drovAnim.SetBool("drovAnim", false);
    }

    private void Update()
    {
        bool isOpen = doorScript.doorAnimator.GetBool("doorOpen");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (isOpen && _dScript.isDoorOpen)
                    {
                        _dScript.ResetMaterial();
                        drovAnim.SetBool("drovAnim", true);
                        if (!nextButton.interactable)
                            nextButton.interactable = true;
                        timerText.text = "�������� ������ � ������� ����� (� ������ '��������')";
                    }
                    else if (!_dScript.isDoorOpen)
                        timerText.text = "�������� ������ ������� ��� �������";
;                }
            }
        }
    }
}
