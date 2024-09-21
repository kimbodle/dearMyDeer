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
        // 카메라의 위치에서 앞 방향으로 레이캐스트 실행
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // F 키를 누르면 상호작용
                if (interactable.RequiresInteraction())
                {
                    InteractiveInfoText.text = $"{hit.collider.name}\nPress F to interact.";
                    DIsplaySignText(interactable);
                }
                else
                {
                    InteractiveInfoText.text = interactable.GetInteractText();
                }

            }
            else
            {
                panel.SetActive(false); // 상호작용 가능한 오브젝트가x
                InteractiveInfoText.text = "";
            }
        }
        else
        {
            panel.SetActive(false); // 레이캐스트가 아무것도 감지x
            InteractiveInfoText.text = "";
        }
        
    }

    private void DIsplaySignText(IInteractable interactable)
    {
        // F 키를 눌렀을 때 상호작용
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<StarterAssets.FirstPersonController>().enabled = true;
            FindObjectOfType<WeaponSwitcher>().enabled = true;
            Time.timeScale = 1f;
            HideSignInfo();
        }
    }

    public void HideSignInfo()
    {
        panel.SetActive(false); // 패널 비활성화
    }
}
