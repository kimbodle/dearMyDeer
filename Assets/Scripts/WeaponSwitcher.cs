using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    //ù��° ���� �⺻ 0
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;
        //�÷��̾ Ư�� Űor ���콺������ �ε��� ��ȯ
        ProcessKeyInput();
        ProcessScrollWheel();

        //�ݺ������� ���� ��ü
        if(previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
        //�������� ����� �̵�
        //���콺 �� �� ����
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //������� �ε��� ��ȣ�� ���߱� ���� - 1
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }

        //���콺 �� �Ʒ� ����. �ٰ��� 0���� ����
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //0�Ǵ� 0���� ���� ���� �Ǿ����� ���̻� ���� �������� ����.
            if (currentWeapon <= 0)
            {
                //���� ���� ������ ���ư�
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
