using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInteractiveInfo : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI signInfoText;
    [SerializeField] private TextMeshProUGUI InteractiveInfoText;

    void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        // ī�޶��� ��ġ���� �� �������� ����ĳ��Ʈ ����
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // F Ű�� ������ ��ȣ�ۿ�
                if (interactable.RequiresInteraction())
                {
                    InteractiveInfoText.text = $"{hit.collider.name}\nF Ű�� ���� ��ȣ�ۿ�";
                    DIsplaySignText(interactable);
                }
                else
                {
                    InteractiveInfoText.text = interactable.GetInteractText();
                }

            }
            else
            {
                panel.SetActive(false); // ��ȣ�ۿ� ������ ������Ʈ��x
                InteractiveInfoText.text = "";
            }
        }
        else
        {
            panel.SetActive(false); // ����ĳ��Ʈ�� �ƹ��͵� ����x
            InteractiveInfoText.text = "";
        }
        
    }

    private void DIsplaySignText(IInteractable interactable)
    {
        // F Ű�� ������ �� ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (interactable != null && interactable.RequiresInteraction())
            {
                Time.timeScale = 0f;
                GetComponent<StarterAssets.FirstPersonController>().enabled = false;
                FindObjectOfType<WeaponSwitcher>().enabled = false;
                panel.SetActive(true);
                Debug.Log("Interacting with the sign");
                signInfoText.text = interactable.GetInteractText();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<StarterAssets.FirstPersonController>().enabled = true;
            FindObjectOfType<WeaponSwitcher>().enabled = true;
            Cursor.visible = false;
            Time.timeScale = 1f;
            HideSignInfo();
        }
    }

    public void HideSignInfo()
    {
        panel.SetActive(false); // �г� ��Ȱ��ȭ
    }
}
