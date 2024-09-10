using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    //첫번째 무기 기본 0
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;
        //플레이어가 특정 키or 마우스를눌러 인덱스 변환
        ProcessKeyInput();
        ProcessScrollWheel();

        //반복문으로 무기 교체
        if(previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
        //음수에서 양수로 이동
        //마우스 휠 위 방향
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //무기들의 인덱스 번호와 맞추기 위해 - 1
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }

        //마우스 휠 아래 방향. 휠값이 0보다 작음
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //0또는 0보다 작은 수가 되었을때 더이상 값이 내려가지 않음.
            if (currentWeapon <= 0)
            {
                //제일 높은 값으로 돌아감
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
