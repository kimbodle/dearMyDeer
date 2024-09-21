using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayInfo : MonoBehaviour
{
    [SerializeField] Canvas gunInfoCanvas;
    [SerializeField] TextMeshProUGUI infoText;

    string range, damage, delay;
    Weapon weapon;

    void Start()
    {
        gunInfoCanvas.enabled = false;
        weapon = GetComponent<Weapon>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale = 0;
            InputControl(false);
            UpdateGunInfo();
            DisplayGunINfo();
        }

        else if (Input.GetKeyUp(KeyCode.F2))
        {
            gunInfoCanvas.enabled = false;
            InputControl(true);
            Time.timeScale = 1;
        }
        UpdateGunInfo();
    }

    private void InputControl(bool isBool)
    {
        GetComponentInParent<StarterAssets.FirstPersonController>().enabled = isBool;
        FindObjectOfType<WeaponSwitcher>().enabled = isBool;
    }

    private void UpdateGunInfo()
    {
        string theGunName = gameObject.name;
        range = weapon.GetRange().ToString();
        damage = weapon.GetDamage().ToString();
        delay = weapon.GetTimeBetweenShots().ToString();
        infoText.text = $" {theGunName}\n Range: {range}\n Damage: {damage}\n Delay: {delay}";

    }

    private void DisplayGunINfo()
    {
        gunInfoCanvas.enabled = true;
    }
}
