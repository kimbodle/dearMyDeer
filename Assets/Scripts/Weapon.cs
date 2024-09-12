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

    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.5f;

    //����ϴ� ������ ź�� ����
    [SerializeField] AmmoType ammoType;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetButtonDown("Fire1")
        if (Input.GetMouseButtonDown(0)&& canShoot ==true)
        {
           StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        //~Ammo(�� �ѿ� �´� ź��)
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
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
