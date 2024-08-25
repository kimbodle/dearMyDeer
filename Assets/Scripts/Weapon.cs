using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera; //ī�޶� ����Ʈ(1��Ī ī�޶�)
    [SerializeField] float range = 100f; //����ĳ������ �󸶳� �ָ����� �� �� �մ���
    [SerializeField] float damage = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        //������ ����? -> �� / �ƴϿ� bool
        //����, ����ĳ��Ʈ ��Ʈ ���� ���� ���� ����
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("I hit this thing: " + hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            { if (target == null) return; } //�� ����
            target.TakeDamage(damage);
        }
        //�ϴ��̳� �� ���� ������ �� ���� ���� ����
        else
        {
            return;
        }
    }
}
