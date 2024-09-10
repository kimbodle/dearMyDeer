using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void handleDeath()
    {
        GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        gameOverCanvas.enabled = true;
        //���Ӱ� ���콺 Ŀ�� �浹 ����
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        //lockState�� CursorLockMode�� ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
