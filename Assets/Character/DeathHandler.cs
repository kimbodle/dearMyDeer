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
        //게임과 마우스 커서 충돌 방지
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        //lockState의 CursorLockMode값 비활성화
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
