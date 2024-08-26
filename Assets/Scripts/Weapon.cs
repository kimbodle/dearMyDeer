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
    [SerializeField] ParticleSystem muzzleFlash;
    //Ư�� ����ü�� �����ٰ� ���� �� ����
    [SerializeField] GameObject hitEffect;
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
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        //������ ����? -> �� / �ƴϿ� bool
        //����, ����ĳ��Ʈ ��Ʈ ���� ���� ���� ����
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            //Debug.Log("I hit this thing: " + hit.transform.name);
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

    private void CreateHitImpact(RaycastHit hit)
    {
        //���� ȿ���� Ư�� ���⿡�� ���̱� ���� -> hit.normal
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
